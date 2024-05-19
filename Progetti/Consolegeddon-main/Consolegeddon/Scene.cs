using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Scene
{
    private List<GameObject> pendingAddObjects = new List<GameObject>();
    private List<GameObject> pendingRemoveObjects = new List<GameObject>();
    private List<GameObject> gameObjects = new List<GameObject>();
    private Dictionary<string, List<GameObject>> tagObjects = new Dictionary<string, List<GameObject>>();
    private Engine engine;
    private Random random = new Random();
    public Scene(Engine engine)
    {
        this.engine = engine;
    }
    public void Update(float dt)
    {
        ApplyPending();
        foreach (var gameObject in gameObjects)
        {
            gameObject.Update(dt);
        }
    }
    public void Render(Renderer renderer)
    {
        foreach(var gameObject in gameObjects)
        {
            gameObject.Render(renderer);
        }
    }
    public void Add(GameObject gameObject)
    {
        pendingAddObjects.Add(gameObject);
        gameObject.Init(this);
    }
    public void ApplyPending()
    {
        foreach (var gameObject in pendingRemoveObjects)
        {
            gameObjects.Remove(gameObject);
        }
        foreach ( var gameObject in pendingAddObjects)
        {
            gameObjects.Add(gameObject);
        }
        foreach (var gameObject in pendingAddObjects)
        {
            gameObject.Start();//WARNING Do not destroy the added objects in the same frame they're added
        }
        pendingAddObjects.Clear();
        pendingRemoveObjects.Clear();
    }
    public void Remove<T>(T gameObject) where T : GameObject
    {
        pendingRemoveObjects.Add(gameObject);
        foreach(string tag in gameObject.tags)
        {
            if (tagObjects.ContainsKey(tag))
                tagObjects[tag].Remove(gameObject);
        }
    }
    public Engine GetEngine()
    {
        return engine;
    }
    public GameObject[] GetGameObjects()
    {
        return gameObjects.ToArray();
    }

    public T[] GetGameObjects<T>(string tag) where T : GameObject
    {
        if (!tagObjects.ContainsKey(tag)) return new T[0];
        T[] array = new T[tagObjects[tag].Count];
        for(int i = 0; i < tagObjects[tag].Count; i++)
        {
            array[i] = (T)tagObjects[tag][i];
        }
        return array;
    }
    public T? GetSingleton<T>(string tag) where T : GameObject
    {
        if (!tagObjects.ContainsKey(tag)) return null;
        return (T)tagObjects[tag][0];
    }
    public void AddTag(string tag, GameObject gameObject)
    {
        if (!tagObjects.ContainsKey(tag))
        {
            tagObjects.Add(tag, new List<GameObject>());
        }
        if ( tagObjects[tag].Contains(gameObject) ) return; //A workaround for spamming addTag to prevent the effect of lag
        tagObjects[tag].Add(gameObject);
        gameObject.tags.Add(tag);
    }
    public T? GetClosest<T>(string tag, float x, float y) where T: GameObject
    {
        T[] objects = GetGameObjects<T>(tag);

        if(objects.Length == 0) return null;

        int closestIndex = 0;
        float closestDistance = float.MaxValue;

        for(int i=0; i < objects.Length; i++)
        {
            float offsetX = objects[i].x - x;
            float offsetY = objects[i].y - y;
            float distanceSqrt = offsetX * offsetX + offsetY * offsetY;

            if(distanceSqrt < closestDistance)
            {
                closestDistance = distanceSqrt;
                closestIndex = i;
            }
        }

        return objects[closestIndex];
    }

    public Random GetRandom()
    {
        return random;
    }
}

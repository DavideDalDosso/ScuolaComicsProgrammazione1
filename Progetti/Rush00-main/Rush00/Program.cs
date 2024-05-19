public class Program
{
    public static void Main(string[] args)
    {
        AdventureEngine adventureEngine = new AdventureEngine();
        adventureEngine.SetPropsPerLine(2);

        Room bedroom = new Room();
        bedroom.SetName("Bedroom");
        bedroom.SetDescription("Rise and shine! Time to get somewhere else!");

        Prop lamp = new Prop();
        lamp.SetName("Lamp");
        lamp.SetDescription("A shimmering piece of plastic that glows.\n" +
            "Good thing it isn't wield by a BIIIIIIIG BIIIIIIIRD.");
        bedroom.AddProp(lamp);

        Prop bed = new Prop();
        bed.SetName("Bed");
        bed.SetDescription("Unfortunately due to the very tight time budget,\n" +
            "I'm not allowed to sleep in it ever again...");
        bedroom.AddProp(bed);

        Prop bongos = new Prop();
        bongos.SetName("Bongos");
        bongos.SetDescription("Bongos pinted :Alien:");
        bedroom.AddProp(bongos);

        Prop window = new Prop();
        window.SetName("Window");
        window.SetDescription("Wow, the world is still an armageddon! Fiery sky...\n" +
            "Decimated cars, screaming people all around and worst of all,\n" +
            "my mailbox got vandalized! Average day in Ohio.");
        bedroom.AddProp(window);

        Room hallway = new Room();
        hallway.SetName("Hallway");
        hallway.SetDescription("Hallways are booooring. They only serve to get somewhere else.\n" +
            "Please give the hallway a meaning to exist and go in another room!");

        Room kitchen = new Room();
        kitchen.SetName("Kitchen");
        kitchen.SetDescription("W H E R E F O O D");

        Prop portal = new Prop();
        portal.SetName("Magic Portal");
        portal.SetDescription("A MAGIC portal that leads straight to my bedroom!\n" +
            "Not the other way around is possible though...");
        kitchen.AddProp(portal);

        Prop lemonade = new Prop();
        lemonade.SetName("Lemonade");
        lemonade.SetDescription("Don't drink that, it's a grenade.\n" +
            "Not good for serenade. But if you have to, it's never lemon late");
        kitchen.AddProp(lemonade);

        Prop porcubbus = new Prop();
        porcubbus.SetName("Porcubbus");
        porcubbus.SetDescription("After you get prickled with your finger, you need to rub your whole\n" +
            "body to feel the same stimulation. It came time for my head to burst. Have a good day.");
        kitchen.AddProp(porcubbus);

        Prop mtndew = new Prop();
        mtndew.SetName("Mountain Dew");
        mtndew.SetDescription("w0t U say1n n00b.\n g3t r3kt 1337 gg no re.");
        kitchen.AddProp(mtndew);

        Prop fridge = new Prop();
        fridge.SetName("Fridge");
        fridge.SetDescription("I open the ridge of the fridge.\n" +
            "It's empty.\n" +
            "Just like my soul.\n" +
            "I can't die cause we ran out of time to implement my\n" +
            "sweet release of death.\n" +
            "Could use some more prosciutto lunchables.");
        kitchen.AddProp(fridge);

        Room livingRoom = new Room();
        livingRoom.SetName("Living Room");
        livingRoom.SetDescription("More like dying room...");

        Prop github = new Prop();
        github.SetName("Github but physically tangible");
        github.SetDescription("I hate this thing lots.");
        livingRoom.AddProp(github);

        Prop sofa = new Prop();
        sofa.SetName("Blank space where Sofa goes");
        sofa.SetDescription("This is where my sofa would go,\n" +
            "If I wasn't in a crippling debt for spending all my bucks in Fortnite.");
        livingRoom.AddProp(sofa);

        Prop silent = new Prop();
        silent.SetName("Silent Orchestra");
        silent.SetDescription("Once all it's member have gathered, all foundations will fall apart.\n" +
            "The orchestra will play a symphony that no one can hear but everyone can listen to.\n" +
            "It's music will pierce your very soul.");
        livingRoom.AddProp(silent);

        Prop tv = new Prop();
        tv.SetName("Television");
        tv.SetDescription("I'm not paying tv taxes.");
        livingRoom.AddProp(tv);

        Prop skeleton = new Prop();
        skeleton.SetName("Skeleton");
        skeleton.SetDescription("\nSpooky\nScary\nSkeletons\nHelping you a\nSkeleTON!\n" +
            "Nyeheheheh. These jokes are rattling my bones");
        livingRoom.AddProp(skeleton);

        Prop nonecld = new Prop();
        nonecld.SetName("Non Euclidean Green Hypercube");
        nonecld.SetDescription("You may not know... but this hefty cube can span across\n" +
            "multiple realities! So you're gonna see the same green hypercube in\n" +
            "multiple rooms!");
        bedroom.AddProp(nonecld);
        kitchen.AddProp(nonecld);
        livingRoom.AddProp(nonecld);
        nonecld.SetOnInteract(() => adventureEngine.SetPreviousActionMessage("If you dared to touch it.\nYou would die to the fifth dimension."));
        nonecld.SetOnPick(() => adventureEngine.SetPreviousActionMessage("You will die if you touch it. I know it blocks the toilet paper but... don't."));

        Prop gun = new Prop();
        gun.SetName("A Gun");
        gun.SetDescription("Ayy,\n Ayy,\n Ayy.\nI've got a gun. no girls, girls gotta die.\n" +
            "Wake up with no hhhuuuueeeeuuuhhh.\n" +
            "Julioioioiohhhhhh.");
        bedroom.AddProp(gun);
        gun.SetOnInteract(() =>
        {
            adventureEngine.SetPreviousActionMessage("You gotta pick it up first cowboi.");
        });
        gun.SetOnPick(() =>
        {
            bedroom.RemoveProp(gun);
            adventureEngine.SetPreviousActionMessage("You are now American.");
            adventureEngine.AddInventoryProp(gun);
        });
        window.SetOnInteract(() =>
        {
            if (adventureEngine.InventoryContains(gun))
                adventureEngine.SetPreviousActionMessage("You shoot to the sky blasting away two ducks and one balloon,\n" +
                    "just because everything was so noisy that asserting your dominance was the power move.");
            else
                adventureEngine.SetPreviousActionMessage("You scream like an asylum maniac so you don't draw any suspicion that you are secretly\n" +
                    "the local Plumber Plunder, pirate of the seven pipes.");
        });
        skeleton.SetOnInteract(() =>
        {
            if (adventureEngine.InventoryContains(gun))
            {
                adventureEngine.SetPreviousActionMessage("DODGE THIS YOU CASUL. You blast the skeleton to dust.");
                Prop dust = new Prop();
                dust.SetName("Skeleton dust");
                dust.SetDescription("Guess I was bad to the bone.");
                livingRoom.AddProp(dust);
                livingRoom.RemoveProp(skeleton);
            }
            else
                adventureEngine.SetPreviousActionMessage("Despite having more flesh and mass, the skeleton taunts you as he's wayyy to nimble for you to handle");
        });


        bedroom.AddExit(hallway);
        hallway.AddExit(bedroom);

        hallway.AddExit(livingRoom);
        livingRoom.AddExit(hallway);

        hallway.AddExit(kitchen);
        kitchen.AddExit(hallway);

        kitchen.AddExit(bedroom);

        kitchen.AddExit(livingRoom);
        livingRoom.AddExit(kitchen);

        adventureEngine.AddRoom(bedroom);
        adventureEngine.AddRoom(hallway);

        adventureEngine.Start(bedroom);
    }
}
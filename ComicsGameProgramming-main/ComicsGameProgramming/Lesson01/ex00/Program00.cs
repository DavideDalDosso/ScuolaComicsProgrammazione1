using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program00
{
    public static void Main()
    {
        //Instancing our custom class alongside the password
        PasswordLock passwordLock = new PasswordLock("Our secret 1337");
        passwordLock.SetAttempts(3);//Set attempts we have unlike the default -1 (infinite attempts)

        Console.WriteLine("Please insert the password: ");//Prompt

        bool over = false;//Our flag for the DO WHILE loop

        passwordLock.SetOnCorrect(() =>
        {
            over = true;
            Console.WriteLine("Congratulations, the password is correct. ");
        });//Success message + end

        passwordLock.SetOnWrong(() =>
        {
            Console.WriteLine("Unfortunately the password is wrong. Please retry... ");
        });//Failure message

        passwordLock.SetOnLockedOut(() =>
        {
            over = true;
            Console.WriteLine("Unfortunately you have ran out of attempts. There's no hope left...");
        });//Locked out message + end

        do
        {

            string input = Util.ReadString();//Read input

            passwordLock.Unlock(input);//If it's wrong, mark for another attempt

        } while (!over);//Retry the procedure if it's wrong
    }

    public class PasswordLock
    {
        private string password;
        private Action onCorrect;
        private Action onWrong;
        private Action onLockedOut;
        private int attempts = -1;
        private int maxAttempts = -1;
        public PasswordLock(string password)
        {
            this.password = password;
        }
        public bool Unlock(string password)//Check if the password equals the parameter
        {
            if(attempts == 0)//We could set attempts to -1 for infinite attempts
            {
                onLockedOut?.Invoke();//The callback may not be implemented so just a quick null-check 'OBJECT?.METHOD'
                return false;
            }

            if (password.Equals(this.password))
            {
                attempts = maxAttempts;//It's the password! Restore all attempts back to max.
                onCorrect?.Invoke();
                return true;
            }
            else
            {
                attempts--;//Wrong password. Deduct one attempt.
                if(attempts == 0) onLockedOut?.Invoke();//We may have used up our last attempt so directly put the locked out callback
                else onWrong?.Invoke();
                return false;
            }
        }
        public void SetOnWrong(Action action)
        {
            onWrong = action;
        }
        public void SetOnCorrect(Action action)
        {
            onCorrect = action;
        }
        public void SetOnLockedOut(Action action)
        {
            onLockedOut = action;
        }
        public void SetAttempts(int attempts)
        {
            this.maxAttempts = attempts;
            this.attempts = maxAttempts;
        }
    }

}

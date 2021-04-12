using System;
using System.Threading.Tasks;

namespace Helpers
{
    public class Awaiter
    {
        public static void WaitFor(Func<bool> condition)
        {
            Task result = Task.Factory.StartNew(() => Awaiting(condition));
            result.Wait();
        }

        private static void Awaiting(Func<bool> condition)
        {
            bool result;
            do
            {
                result = TryInvokeCondition(condition);
                Task.Delay(100).Wait();
            } while (!result);
        }

        private static bool TryInvokeCondition(Func<bool> condition)
        {
            try
            {
                return condition.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception($"There was something wrong:{Environment.CommandLine}{e.Message}");
            }
        }
    }

    public class Awaiter<T> where T : class
    {
        public static T WaitFor(Func<T> func)
        {
            Task<T> result = Task.Factory.StartNew(() => Awaiting(func));
            result.Wait();
            return result.Result;
        }

        private static T Awaiting(Func<T> func)
        {
            T result;
            do
            {
                result = TryInvokeFunc(func);
                Task.Delay(100).Wait();
            } while (result == null);

            return result;
        }

        private static T TryInvokeFunc(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception($"There was something wrong:{Environment.CommandLine}{e.Message}");
            }
        }
    }
}
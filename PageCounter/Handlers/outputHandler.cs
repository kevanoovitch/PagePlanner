using PageCounter.Data;

namespace PageCounter.Handlers
{
    public class OutputHandler(Result sharedResult)
    {
        private Result _result = sharedResult;

        public void PrintResult()
        {
            foreach (var kvp in _result.ResultPlan)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} \n");
            }
        }
    }
}

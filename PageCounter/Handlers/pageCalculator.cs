using PageCounter.Data;

namespace PageCounter.Handlers // eller valfritt namespace
{
    public class PageCalculator(UserInputParams sharedParams)
    {
        public Result GetResults()
        {
            return _calcResults;
        }

        private UserInputParams _params = sharedParams;
        private Result _calcResults = new();

        private int CalculateAmountOfDays()
        {
            TimeSpan timeLeft = _params.DtEndDate - DateTime.Now;

            int daysLeft = timeLeft.Days;

            return daysLeft;
        }

        private Dictionary<DateTime, int> PlanPagesPerDay(int pagesPerDay, int leftOverPages)
        {
            var plan = new Dictionary<DateTime, int>();

            for (var day = DateTime.Now.Date; day <= _params.DtEndDate; day = day.AddDays(1))
            {
                int todaysPages = pagesPerDay;

                // create a a entry and put it into the dic

                if (day == _params.DtEndDate)
                {
                    //on last day add left leftOverPages
                    todaysPages += leftOverPages;
                }

                plan.Add(day, todaysPages);
            }
            return plan;
        }

        public void Calculate()
        {
            // calculate total amount of days
            int daysLeft = CalculateAmountOfDays();
            // amount // pages = perDay

            int pagesPerDay = _params.BookLength / daysLeft;

            int leftoverPages = _params.BookLength % daysLeft;

            //Plan and plot the pages per day

            _calcResults.ResultPlan = PlanPagesPerDay(pagesPerDay, leftoverPages);

            return;
        }
    }
}

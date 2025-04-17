using PageCounter.Data;

namespace PageCounter.Handlers // eller valfritt namespace
{
    public class PageCalculator(UserInputParams sharedParams)
    {
        public UserInputParams parmas = sharedParams;
        public CalculatedPagePlan calcResults = new();

        private int CalculateAmountOfDays()
        {
            TimeSpan timeLeft = parmas.DtEndDate - DateTime.Now;

            int daysLeft = timeLeft.Days;

            return daysLeft;
        }

        private Dictionary<DateTime, int> PlanPagesPerDay(int pagesPerDay, int leftOverPages)
        {
            var plan = new Dictionary<DateTime, int>();

            for (var day = DateTime.Now.Date; day <= parmas.DtEndDate; day = day.AddDays(1))
            {
                int todaysPages = pagesPerDay;

                // create a a entry and put it into the dic

                if (day == parmas.DtEndDate)
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

            int pagesPerDay = parmas.BookLength / daysLeft;

            int leftoverPages = parmas.BookLength % daysLeft;

            //Plan and plot the pages per day

            calcResults.ResultPlan = PlanPagesPerDay(pagesPerDay, leftoverPages);

            return;
        }
    }
}

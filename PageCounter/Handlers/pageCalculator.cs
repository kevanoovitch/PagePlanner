using PageCounter.Data;

namespace PageCounter.Handlers // eller valfritt namespace
{
    public class PageCalculator(UserInputParams sharedParams)
    {
        //do something

        private UserInputParams _params = sharedParams;

        public void Calculate()
        {
            // calculate total amount of days
            // amount // pages = perDay
            // plot this???
            return;
        }
    }
}

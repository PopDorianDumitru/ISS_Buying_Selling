namespace Lab2Validator
{
    internal class Program
    {
        static void LucaMain(string[] args)
        {
            Validator validator = new Validator();

            try
            {
                validator.validateDate("06/06/2006");
            } catch(ValidatorException e)
            {
                List<string> strings = e.getErrors();
                foreach(string s in strings)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}

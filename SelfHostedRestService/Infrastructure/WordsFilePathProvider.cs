namespace SelfHostedRestService.Infrastructure
{
    public class WordsFilePathProvider
    {
        public string GetPath()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["WordsFilePath"];

            return "words.txt";
        }
    }
}

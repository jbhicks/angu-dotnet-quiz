using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Quiz.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        private readonly IFileProvider _fileProvider;
        ILogger<QuizController> _logger;
        private string[] quizArray;
        private JObject jsonQuiz;
        
        public QuizController(IFileProvider fileProvider, 
                              ILogger<QuizController> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
            quizArray = new string[1];
            string rawFileInfo;
            IFileInfo fileInfo = _fileProvider.GetFileInfo("./data/LSA.json");
            Stream fileInfoStream = fileInfo.CreateReadStream();
            StreamReader reader = new StreamReader(fileInfoStream);
            rawFileInfo = new StreamReader(fileInfoStream).ReadToEnd();
            reader.Dispose();
            quizArray[0] = rawFileInfo;

            jsonQuiz = JObject.Parse(rawFileInfo);

            logContents(rawFileInfo);
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return quizArray;
            //return new string[] { "" };
        }

        private void logContents(string fileInfo)
        {
            _logger.LogDebug("_______________________________________");
            _logger.LogDebug("");
            _logger.LogDebug(fileInfo);
            _logger.LogDebug("_______________________________________");
        }

        public class Quiz
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

        }



    }
    
}
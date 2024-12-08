using OpenAI.Chat;
using OpenAIDemo;
using System.Diagnostics;

namespace ToreAurstadIT.OpenAIDemo
{
    public class ChatGptDemo
    {

        public async Task<string?> RunChatGptQuery(ChatClient? chatClient, string msg)
        {
            if (chatClient == null)
            {
                Console.WriteLine("Sorry, the demo failed. The chatClient did not initialize propertly.");
                return null;
            }

            Console.WriteLine("Searching ... Please wait..");

            var stopWatch = Stopwatch.StartNew();

            var chatDataSources = new[]{
                (
                    SearchEndPoint: Environment.GetEnvironmentVariable("AZURE_SEARCH_AI_ENDPOINT", EnvironmentVariableTarget.User) ?? "N/A",
                    SearchIndexName: Environment.GetEnvironmentVariable("AZURE_SEARCH_AI_INDEXNAME", EnvironmentVariableTarget.User) ?? "N/A",
                    SearchApiKey: Environment.GetEnvironmentVariable("AZURE_SEARCH_AI_APIKEY", EnvironmentVariableTarget.User) ?? "N/A"
                )
            };

            string reply = "";

            try
            {

                reply = await chatClient.GetStreamedReplyStringAsync(msg, dataSources: chatDataSources, outputToConsole: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine($"The operation took: {stopWatch.ElapsedMilliseconds} ms");


            Console.WriteLine();

            return reply;
        }

    }
}

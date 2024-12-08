using System.Diagnostics;

namespace ToreAurstadIT.OpenAIDemo
{

    public class Program
    {

        public static async Task Main(string[] args)
        {
            const string modelName = "gpt-4";

        
            Console.WriteLine("Welcome to the Azure AI Chat GPT-4 demo");

            do
            {
                var chatClient = AzureOpenAIClientBuilder
                    .Instance
                    .WithDefaultEndpointFromEnvironmentVariable()
                    .WithDefaultKeyFromEnvironmentVariable()
                    .BuildChatClient(aiModel: modelName);

                var chatGptDemo = new ChatGptDemo();

                string? reply = await RunOpenAiQuery(chatClient, chatGptDemo);
                #region TraceOutput
                Trace.WriteLine("==================================================================");
                Trace.WriteLine("\nGot the following result back from the Azure AI Chat GPT-4 demo:");
                Trace.WriteLine($"\n{reply}");
                #endregion
                Console.WriteLine("Do you want to eXit this program? Press 'X'. Press any button to continue.");
                chatClient = null;
            }
            while ((Console.ReadKey().Key != ConsoleKey.X));

            Console.WriteLine("All done. Exiting.");
        }

        private static async Task<string?> RunOpenAiQuery(OpenAI.Chat.ChatClient? chatClient, ChatGptDemo chatGptDemo)
        {
            Console.WriteLine("\nQuery: (hit Enter to continue)");

            string? reply = null;

            string? msg = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(msg))
            {
                reply = await chatGptDemo.RunChatGptQuery(chatClient, msg);
            }
            else
            {
                Console.WriteLine("Enter your question and hit Enter.");
            }

            return reply;
        }

    }
}

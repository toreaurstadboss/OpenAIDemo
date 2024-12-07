using OpenAIDemo;

namespace ToreAurstadIT.OpenAIDemo
{

    public class Program
    {

        public static async Task Main(string[] args)
        {

            const string modelName = "gpt-4";

            var chatClient = AzureOpenAIClientBuilder
                .Instance
                .WithDefaultEndpointFromEnvironmentVariable()
                .WithDefaultKeyFromEnvironmentVariable()
                .BuildChatClient(aiModel: modelName);

            var chatGptDemo = new ChatGptDemo();

            Console.WriteLine("Welcome to the Azure AI Chat GPT-4 demo");
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

            Console.WriteLine("==================================================================");
            Console.WriteLine("\nGot the following result back from the Azure AI Chat GPT-4 demo:");

            Console.WriteLine($"\n{reply}");

            Console.WriteLine("Press any key to continue..");
            //  Console.ReadKey();
        }


    }
}

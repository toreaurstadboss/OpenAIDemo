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

            var stopWatch = Stopwatch.StartNew();

            string reply = await chatClient.GetStreamedReplyStringAsync(msg, outputToConsole: true);

            Console.WriteLine($"The operation took: {stopWatch.ElapsedMilliseconds} ms");


            Console.WriteLine();

            return reply;
        }

    }
}

using OpenAI.Chat;
using System.ClientModel;
using System.Text;

namespace OpenAIDemo
{
    public static class ChatclientExtensions
    {

        /// <summary>
        /// Provides a stream result from the Chatclient service using AzureAI services.
        /// </summary>
        /// <param name="chatClient">ChatClient instance</param>
        /// <param name="message">The message to send and communicate to the ai-model</param>
        /// <returns>Streamed chat reply / result. Consume using 'await foreach'</returns>
        public static AsyncCollectionResult<StreamingChatCompletionUpdate> GetStreamedReplyAsync(this ChatClient chatClient, string message) =>
            chatClient.CompleteChatStreamingAsync(
                [new SystemChatMessage("You are an helpful, wonderful AI assistant"), new UserChatMessage(message)]);

        public static async Task<string> GetStreamedReplyStringAsync(this ChatClient chatClient, string message, bool outputToConsole = false)
        {
            var sb = new StringBuilder();
            await foreach (var update in GetStreamedReplyAsync(chatClient, message))
            {
                foreach (var textReply in update.ContentUpdate.Select(cu => cu.Text))
                {
                    sb.Append(textReply);
                    if (outputToConsole)
                    {
                        Console.Write(textReply);
                    }
                }
            }
            return sb.ToString();
        }

    }
}

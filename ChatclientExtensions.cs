using Azure.AI.OpenAI.Chat;
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
        public static AsyncCollectionResult<StreamingChatCompletionUpdate> GetStreamedReplyAsync(this ChatClient chatClient, string message,
            (string endpoint, string indexname, string authentication)[]? dataSources = null)
        {
            ChatCompletionOptions? chatCompletionOptions = null;
            if (dataSources?.Any() == true)
            {
                chatCompletionOptions = new ChatCompletionOptions();
#pragma warning disable AOAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

                foreach (var dataSource in dataSources!)
                {
                    chatCompletionOptions.AddDataSource(new AzureSearchChatDataSource()
                    {
                        Endpoint = new Uri(dataSource.endpoint),
                        IndexName = dataSource.indexname,
                        Authentication = DataSourceAuthentication.FromApiKey(dataSource.authentication)
                    });
                }
#pragma warning restore AOAI001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
            }

            return chatClient.CompleteChatStreamingAsync(
                [new SystemChatMessage("You are an helpful, wonderful AI assistant"), new UserChatMessage(message)], chatCompletionOptions);
        }

        public static async Task<string> GetStreamedReplyStringAsync(this ChatClient chatClient, string message, (string endpoint, string indexname, string authentication)[]? dataSources = null, bool outputToConsole = false)
        {
            var sb = new StringBuilder();
            await foreach (var update in GetStreamedReplyAsync(chatClient, message, dataSources))
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

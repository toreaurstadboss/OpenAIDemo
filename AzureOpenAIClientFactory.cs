using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.ClientModel;

namespace ToreAurstadIT.OpenAIDemo
{

    /// <summary>
    /// Creates AzureOpenAIClient or ChatClient (default model is "gpt-4")
    /// Suggestion:
    /// Create user-specific Environment variables for : AZURE_AI_SERVICES_KEY and AZURE_AI_SERVICES_ENDPOINT to avoid exposing endpoint and key in source code.
    /// Then us the 'WithDefault' methods to use the two user-specific environment variables, which must be set.
    /// </summary>
    public class AzureOpenAIClientBuilder
    {

        private const string AZURE_AI_SERVICES_KEY = nameof(AZURE_AI_SERVICES_KEY);
        private const string AZURE_AI_SERVICES_ENDPOINT = nameof(AZURE_AI_SERVICES_ENDPOINT);

        private string? _endpoint = null;
        private ApiKeyCredential? _key = null;

        public AzureOpenAIClientBuilder WithEndpoint(string endpoint) { _endpoint = endpoint; return this; }

        /// <summary>
        /// Usage: Provide user-specific enviornment variable called : 'AZURE_AI_SERVICES_ENDPOINT'
        /// </summary>
        /// <returns></returns>
        public AzureOpenAIClientBuilder WithDefaultEndpointFromEnvironmentVariable() { _endpoint = Environment.GetEnvironmentVariable(AZURE_AI_SERVICES_ENDPOINT, EnvironmentVariableTarget.User); return this; }
       
        
        public AzureOpenAIClientBuilder WithKey(string key) { _key = new ApiKeyCredential(key); return this; }       
        public AzureOpenAIClientBuilder WithKeyFromEnvironmentVariable(string key) { _key = new ApiKeyCredential(Environment.GetEnvironmentVariable(key) ?? "N/A"); return this; }

        /// <summary>
        /// Usage : Provide user-specific environment variable called : 'AZURE_AI_SERVICES_KEY'
        /// </summary>
        /// <returns></returns>
        public AzureOpenAIClientBuilder WithDefaultKeyFromEnvironmentVariable() { _key = new ApiKeyCredential(Environment.GetEnvironmentVariable(AZURE_AI_SERVICES_KEY, EnvironmentVariableTarget.User) ?? "N/A"); return this; }

        public AzureOpenAIClient? Build() => !string.IsNullOrWhiteSpace(_endpoint) && _key != null ? new AzureOpenAIClient(new Uri(_endpoint), _key) : null;

        /// <summary>
        /// Default model will be set 'gpt-4'
        /// </summary>
        /// <returns></returns>
        public ChatClient? BuildChatClient(string aiModel = "gpt-4") => Build()?.GetChatClient(aiModel);

        public static AzureOpenAIClientBuilder Instance => new AzureOpenAIClientBuilder();

    }
}

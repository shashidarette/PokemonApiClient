﻿using System;
using Pokemon.Interfaces;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokemon.DataModel;

namespace Pokemon.Client
{
    public class PokemonYodaClient : IYodaTranslation
    {
        HttpClient _translateClient = new HttpClient() { BaseAddress = new Uri("https://api.funtranslations.com/translate/") };

        public async Task<string> GetPokemonTranslation(string data)
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "/translate/yoda.json");
            var keyValData = new Dictionary<string, string>();
            keyValData.Add("text", data);
            request.Content = new FormUrlEncodedContent(keyValData);
            
            using var response = await _translateClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, System.Threading.CancellationToken.None);
            if (response.IsSuccessStatusCode)
            {
                TranslationDto translation = TranslationDto.DeserializeStream(await response.Content.ReadAsStreamAsync());
                if (translation.contents != null)
                {
                    return translation.contents.translated;
                }
            }
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;


namespace TrashSnap
{
    public static class API
    {
        private static string _baseUrl = "http://greenplease.com" + "/api/v1/";
        static string BaseUrl { get {
                return _baseUrl;
            }
        }

        /// <summary>
        /// Prenese entry na server, return = uspelo ali ne
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>Uspeh</returns>
        static bool UploadEntry(Entry entry)
        {
            try
            {
                var imageStream = new ByteArrayContent(entry.Photo);
                imageStream.Headers.ContentDisposition = new ContentDispositionHeaderValue("photo")
                {
                    FileName = Guid.NewGuid() + ".jpg"
                };
                var contentOtherParameters = new FormUrlEncodedContent(entry.Values());

                var multi = new MultipartContent();
                multi.Add(imageStream);
                multi.Add(contentOtherParameters); // I guess it should work?
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                var result = client.PostAsync("upload", multi).Result;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

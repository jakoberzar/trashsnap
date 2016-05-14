using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TrashSnap
{
    public static class API
    {
        private static string _baseUrl = "http://trashsnap-jakoberzar.c9users.io"; // /api/v1/ gets added
        static string BaseUrl { get {
                return _baseUrl + "/api/v1/";
            }
        }

        /// <summary>
        /// Prenese vsa polja, vendar brez slike!!!!! Use UploadPhoto instead, ta je samo za arhiv
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static bool UploadEntry(Entry entry) 
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                var contentOtherParameters = new FormUrlEncodedContent(entry.Values());
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                var result = client.PostAsync("upload", contentOtherParameters).Result;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Prenese entry skupaj s sliko na server, return = uspelo ali ne
        /// </summary>
        /// <param name="entry"></param>
        /// <returns>Uspeh</returns>
        public async static Task<bool> UploadPhoto(Entry entry)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                StreamContent scontent = new StreamContent(entry.PhotoStream);
                scontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = entry.PhotoId + ".jpg",
                    Name = "image"
                };
                var client = new HttpClient();
                var multi = new MultipartFormDataContent();
                multi.Add(scontent);
                // Dodaj še ostale parametre
                foreach (var item in entry.Values())
                {
                    multi.Add(new StringContent(item.Value), item.Key);
                }
                client.BaseAddress = new Uri(BaseUrl);
                var result = await client.PostAsync("upload/photo", multi);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

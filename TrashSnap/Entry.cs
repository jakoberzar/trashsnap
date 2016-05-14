using System;
using System.Collections.Generic;
using System.Text;

namespace TrashSnap
{
    public class Entry
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Text { get; set; }
        public byte[] Photo { get; set; }

        /// <summary>
        /// Vrne čas vnosa kot string, v formatu YYYY-MM-DD HH:MM:SS. (za API)
        /// Le getter, dokler le uploadamo, potem pa bo tudi setter.
        /// </summary>
        public string Time {
            get {
                return TimeDate.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// Čas vnosa kot DateTime. Get (za app, ne api) in set.
        /// </summary>
        public DateTime TimeDate {  get; set; }
        
        public Entry() { }
        public Entry(string text, float longitude, float latitude, byte[] photo) : this(text, longitude, latitude, photo, DateTime.Now) { }
        public Entry(string text, float longitude, float latitude, byte[] photo, DateTime time)
        {
            this.Text = text;
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Photo = photo;
            this.TimeDate = time;
        }

        public Dictionary<string, string> Values()
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("longitude", Longitude.ToString());
            d.Add("latitude", Latitude.ToString());
            d.Add("text", Text);
            return d;
        }
    }
}

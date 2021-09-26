using Emgu.CV;
using monitors_comunication.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace monitors_comunication.Models
{
    public class VitalSigns<T> : IMonitor<T>
    {

        private T _cardiacFrecuency;
        private T _saturation;
        private T _nonNisvasiveBloodPressure;
        

        public T CardiacFrecuency { get => _cardiacFrecuency; set => _cardiacFrecuency = value; }
        public T Saturation { get => _saturation; set => _saturation = value; }
        public T NonNisvasiveBloodPressure { get => _nonNisvasiveBloodPressure; set => _nonNisvasiveBloodPressure = value; }

        public Dictionary<String,T> ObtainDataMonitor() {

            Dictionary<string, T> vitalSigns = new()
            {
                { Configuration.Configuration.CARDIAC_FRECUENCY, _cardiacFrecuency },
                { Configuration.Configuration.SATURATION, _saturation },
                { Configuration.Configuration.NON_INVASIVE_BLOOD_PRESURE, _nonNisvasiveBloodPressure }
            };

            return vitalSigns;
        
        }
        public int SaveDataMonitor(Dictionary<String, T> dataMonitor)
        {
            int signsSaved = 0;
            if (dataMonitor.Count < 0)
            {
                return -1;
            }
            foreach(var data in dataMonitor){
                if(Configuration.Configuration.CARDIAC_FRECUENCY.Equals(data.Key))
                {
                    _cardiacFrecuency = data.Value;
                }
                if (Configuration.Configuration.NON_INVASIVE_BLOOD_PRESURE.Equals(data.Key )) {
                    _nonNisvasiveBloodPressure = data.Value;
                }
                if (Configuration.Configuration.SATURATION.Equals(data.Key)) {
                    _saturation = data.Value;
                }

            }

            return signsSaved;
        }
    }
}

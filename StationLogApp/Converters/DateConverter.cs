using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationLogApp.Converters
{
    public class DateConverter
    {
        public DateTime ConvertToDate(DateTimeOffset dateToConvert)
        {
            return dateToConvert.DateTime;
        }



    }
}

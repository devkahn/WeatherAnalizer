using DLWIZ.Defines.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mPublicDailyWeatherData
    {
        [ColumnHeader("Idx")]
        public int Idx { get; set; } = -1;


        [ColumnHeader("DateYear")]
        [Description("관측일-년")]
        public int DateYear { get; set; }

        [ColumnHeader("DateMonth")]
        [Description("관측일-월")]
        public int DateMonth { get; set; }

        [ColumnHeader("DateDay")]
        [Description("관측일-일")]
        public int DateDay { get; set; }

        [ColumnHeader("StationCode")]
        [Description("관측 지점 번호")]
        public int StationCode { get; set; } = -1;


        [ColumnHeader("windSpeed_MAX")]
        [Description("최대 풍속")]
        public double WindSpeed_Max { get; set; } = -1.0;


        [ColumnHeader("windSpeed_MAX_Time")]
        [Description("최대 풍속")]
        public int WindSpeed_Max_Time { get; set; } = -1;


        [ColumnHeader("windDirection_MAX")]
        [Description("최대 풍향")]
        public int WindDirection_Max { get; set; } = -1;


        [ColumnHeader("temperature_Ave")]
        [Description("평균 기온")]
        public double Temperature_Ave { get; set; } = -1.0;


        [ColumnHeader("temperature_Max")]
        [Description("최고 기온")]
        public double Temperature_Max { get; set; } = -1.0;


        [ColumnHeader("temperature_Max_Time")]
        [Description("최고 기온 시간")]
        public int Temperature_Max_Time { get; set; } = -1;


        [ColumnHeader("temperature_Min")]
        [Description("최저 기온")]
        public double Temperature_Min { get; set; } = -1.0;


        [ColumnHeader("temperature_Min_Time")]
        [Description("최저 기온 시간")]
        public int Temperature_Min_Time { get; set; }

        [ColumnHeader("rain_Total")]
        [Description("일일 강수량")]
        public double Rain_Total { get; set; }

        [ColumnHeader("rain_DayTotal")]
        [Description("9-9 강수량")]
        public double Rain_99Total { get; set; }

        [ColumnHeader("snow_Total_Depth")]
        [Description("누적 적설량 - 이전 부터 녹지 않은 눈의 두께까지 포함")]
        public double Snow_Total_Depth { get; set; }

        [ColumnHeader("snow_Total_Daily_Depth")]
        [Description("당일 적설량 - 당일 내린 눈만 측정한 적설량")]
        public double Snow_Daily_Total_Depth { get; set; }
    }
}

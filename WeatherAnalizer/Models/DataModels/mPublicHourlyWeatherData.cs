using DLWIZ.Common.Models.ProgramModels.Attributes;
using DLWIZ.Defines.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAnalizer.Models.DataModels
{
    public class mPublicHourlyWeatherData
    {
        [Inteager(0)]
        [ColumnHeader("Idx")]
        [Description("Index")]
        public int Idx { get; set; } = -1;


        [Inteager(1)]
        [ColumnHeader("TM")]
        [Description("관측시각(KST)")]
        public DateTime ObserveTime { get; set; } = DateTime.MinValue;


        [Inteager(2)]
        [ColumnHeader("STN")]
        [Description("관측소 지점번호")]
        public int StationCode { get; set; } = -1;


        [Inteager(3)]
        [ColumnHeader("WD")]
        [Description("풍향 (16방위)")]
        public int? WindDirection { get; set; } = null;


        [Inteager(4)]
        [ColumnHeader("WS")]
        [Description("풍속 (m/s)")]
        public double? WindSpeed { get; set; } = null;


        [Inteager(5)]
        [ColumnHeader("GST_WD")]
        [Description("돌풍향 (16방위)")]
        public int? GustWindDirection { get; set; } = null;


        [Inteager(6)]
        [ColumnHeader("GST_WS")]
        [Description("돌풍속 (m/s)")]
        public double? GustWindSpeed { get; set; } = null;


        [Inteager(7)]
        [ColumnHeader("GST_TM")]
        [Description("돌풍속이 관측된 시각 (시분)")]
        public int? GustWindTime { get; set; } = null;


        [Inteager(8)]
        [ColumnHeader("PA")]
        [Description("현지기압 (hPa)")]
        public double? PressureByAtomospheric { get; set; } = null;


        [Inteager(9)]
        [ColumnHeader("PS")]
        [Description("해면기압 (hPa)")]
        public double? PressureBySeaLevel { get; set; } = null;


        [Inteager(10)]
        [ColumnHeader("PT")]
        [Description("기압변화경향 (Code 0200)")]
        public int? PressureChangeTendency { get; set; } = null;


        [Inteager(11)]
        [ColumnHeader("PR")]
        [Description("기압변화량 (hPa)")]
        public double? PressureChange { get; set; } = null;


        [Inteager(12)]
        [ColumnHeader("TA")]
        [Description("기온 (C)")]
        public double? TemperatureAtomospheric { get; set; } = null;


        [Inteager(13)]
        [ColumnHeader("TD")]
        [Description("이슬점온도 (C)")]
        public double? TemperatureDewPoint { get; set; } = null;


        [Inteager(14)]
        [ColumnHeader("HM")]
        [Description("상대습도 (%)")]
        public double? Humidity { get; set; } = null;


        [Inteager(15)]
        [ColumnHeader("PV")]
        [Description("수증기압 (hPa)")]
        public double? PressureByVapor { get; set; } = null;


        [Inteager(16)]
        [ColumnHeader("RN")]
        [Description("강수량 (mm) : 여름철에는 1시간강수량, 겨울철에는 3시간강수량")]
        public double? Rainfall { get; set; } = null;


        [Inteager(17)]
        [ColumnHeader("RN_DAY")]
        [Description("일강수량 (mm) : 해당시간까지 관측된 양(통계표)")]
        public double? RainfallOfDayByStatics { get; set; } = null;


        [Inteager(18)]
        [ColumnHeader("RN_JUN")]
        [Description("일강수량 (mm) : 해당시간까지 관측된 양을 전문으로 입력한 값(전문)")]
        public double? RainfallOfDayBySpecial { get; set; } = null;


        [Inteager(19)]
        [ColumnHeader("RN_INT")]
        [Description("강수강도 (mm/h) : 관측하는 곳이 별로 없음")]
        public double? RainfallOfDayByInternational { get; set; } = null;


        [Inteager(20)]
        [ColumnHeader("SD_HR3")]
        [Description("3시간 신적설 (cm) : 3시간 동안 내린 신적설의 높이")]
        public double? SnowDepthOfHour3 { get; set; } = null;


        [Inteager(21)]
        [ColumnHeader("SD_DAY")]
        [Description("일 신적설 (cm) : 00시00분부터 위 관측시간까지 내린 신적설의 높이")]
        public double? SnowDepthOfDay { get; set; } = null;


        [Inteager(22)]
        [ColumnHeader("SD_TOT")]
        [Description("적설 (cm) : 치우지 않고 그냥 계속 쌓이도록 놔눈 경우의 적설의 높이")]
        public double? SnowDepth { get; set; } = null;


        [Inteager(23)]
        [ColumnHeader("WC")]
        [Description("GTS 현재일기 (Code 4677)")]
        public int? WC { get; set; } = null;


        [Inteager(24)]
        [ColumnHeader("WP")]
        [Description("GTS 과거일기 (Code 4561) .. 3(황사),4(안개),5(가랑비),6(비),7(눈),8(소나기),9(뇌전)")]
        public int? WP { get; set; } = null;


        [Inteager(25)]
        [ColumnHeader("WW")]
        [Description("국내식 일기코드 (문자열 22개) : 2자리씩 11개까지 기록 가능 (코드는 기상자원과 문의)")]
        public int? WW { get; set; } = null;


        [Inteager(26)]
        [ColumnHeader("CA_TOT")]
        [Description("전운량 (1/10)")]
        public int? CloudAmountTotal { get; set; } = null;


        [Inteager(27)]
        [ColumnHeader("CA_MID")]
        [Description("중하층운량 (1/10)")]
        public int? CloudAmountMiddle { get; set; } = null;


        [Inteager(28)]
        [ColumnHeader("CH_MIN")]
        [Description("최저운고 (100m)")]
        public int? CloudHeightMinimum { get; set; } = null;


        [Inteager(29)]
        [ColumnHeader("CT")]
        [Description("운형 (문자열 8개) : 2자리 코드로 4개까지 기록 가능")]
        public string CloudType { get; set; } = null;


        [Inteager(30)]
        [ColumnHeader("CT_TOP")]
        [Description("GTS 상층운형 (Code 0509)")]
        public int? CloudTypeTop { get; set; } = null;


        [Inteager(31)]
        [ColumnHeader("CT_MID")]
        [Description("GTS 중층운형 (Code 0515)")]
        public int? CloudTypeMiddle { get; set; } = null;


        [Inteager(32)]
        [ColumnHeader("CT_LOW")]
        [Description("GTS 하층운형 (Code 0513)")]
        public int? CloudTypeLow { get; set; } = null;


        [Inteager(33)]
        [ColumnHeader("VS")]
        [Description("시정 (10m)")]
        public int? ViewDistance { get; set; } = null;


        [Inteager(34)]
        [ColumnHeader("SS")]
        [Description("일조 (hr)")]
        public double? Sunshine { get; set; } = null;


        [Inteager(35)]
        [ColumnHeader("SI")]
        [Description("일사 (MJ/m2)")]
        public double? SolarIrradiance { get; set; } = null;


        [Inteager(36)]
        [ColumnHeader("ST_GD")]
        [Description("지면상태 코드 (코드는 기상자원과 문의)")]
        public int? StyleOfGround { get; set; } = null;


        [Inteager(37)]
        [ColumnHeader("TS")]
        [Description("지면온도 (C)")]
        public double? TemperatureOfGround { get; set; } = null;


        [Inteager(38)]
        [ColumnHeader("TE_005")]
        [Description("5cm 지중온도 (C)")]
        public double? TemperatureOfEarth5cm { get; set; } = null;


        [Inteager(39)]
        [ColumnHeader("TE_01")]
        [Description("10cm 지중온도 (C)")]
        public double? TemperatureOfEarth10cm { get; set; } = null;


        [Inteager(40)]
        [ColumnHeader("TE_02")]
        [Description("20cm 지중온도 (C)")]
        public double? TemperatureOfEarth20cm { get; set; } = null;


        [Inteager(41)]
        [ColumnHeader("TE_03")]
        [Description("30cm 지중온도 (C)")]
        public double? TemperatureOfEarth30cm { get; set; } = null;


        [Inteager(42)]
        [ColumnHeader("ST_SEA")]
        [Description("해면상태 코드 (코드는 기상자원과 문의)")]
        public int? StatusOfSeaSurface { get; set; } = null;


        [Inteager(43)]
        [ColumnHeader("WH")]
        [Description("파고 (m) : 해안관측소에서 목측한 값")]
        public double? WaveHeight { get; set; } = null;


        [Inteager(44)]
        [ColumnHeader("BF")]
        [Description("Beaufart 최대풍력(GTS코드)")]
        public int? BeaufortOfWindSpeedMax { get; set; } = null;


        [Inteager(45)]
        [ColumnHeader("IR")]
        [Description("강수자료 유무 (Code 1819) .. 1(Sec1에 포함), 2(Sec3에 포함), 3(무강수), 4(결측)")]
        public int? HasRainfulData { get; set; } = null;


        [Inteager(46)]
        [ColumnHeader("IX")]
        [Description("유인관측/무인관측 및 일기 포함여부 (code 1860) .. 1,2,3(유인) 4,5,6(무인) / 1,4(포함), 2,5(생략), 3,6(결측)")]
        public int? IncludeObservationData { get; set; } = null;

    }
}

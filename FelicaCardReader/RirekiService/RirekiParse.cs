using System;
using System.Collections.Generic;

namespace FelicaCardReader.RirekiService
{
    public class RirekiParse
    {

        private int termId = 0;
        private int procId = 0;
        private int year = 0;
        private int month = 0;
        private int day = 0;
        private int remain = 0;
        private int admissionStationCode = 0;
        private int admissionStationSeq = 0;
        private int participationStationCode = 0;
        private int participationStationSeq = 0;

        private byte[] rirekiData;
        public RirekiParse(byte[] _rirekiData)
        {
            rirekiData = _rirekiData;
            Init();
        }

        private void Init()
        {
            termId = (int)rirekiData[0];
            procId = (int)rirekiData[1];

            var mixInt = ToInt(rirekiData, 4, 5);
            year = mixInt >> 9 & 0x07f;
            month = mixInt >> 5 & 0x00f;
            day = mixInt & 0x01f;

            remain = ToInt(rirekiData, 11, 10);

        }

        private int ToInt(byte[] res, params int[] idx)
        {
            var num = 0;
            foreach (var i in idx)
            {
                num = num << 8;
                num += (int)res[i] & 0x0ff;
            }
            return num;
        }

        public string ToString()
        {
            return 
                $"機器種別：{TERM_MAP[termId]}\n" +
                $"利用種別：{PROC_MAP[procId]}\n" +
                $"日付：{year}/{month}/{day}\n" +
                $"残額：{remain}\n\n";
        }

        private Dictionary<int, string> TERM_MAP = new Dictionary<int, string>()
        {
            {3, "精算機"},
            {4, "携帯型端末"},
            {5, "車載端末"},
            {7, "券売機"},
            {8, "券売機"},
            {9, "入金機"},
            {18, "券売機"},
            {20, "券売機等"},
            {21, "券売機等"},
            {22, "改札機"},
            {23, "簡易改札機"},
            {24, "窓口端末"},
            {25, "窓口端末"},
            {26, "改札端末"},
            {27, "携帯電話"},
            {28, "乗継精算機"},
            {29, "連絡改札機"},
            {31, "簡易入金機"},
            {70, "VIEW ALTTE"},
            {72, "VIEW ALTTE"},
            {199, "物販端末"},
            {200, "自販機"}
        };

        private Dictionary<int, string> PROC_MAP = new Dictionary<int, string>()
        {
            {1, "運賃支払(改札出場)"},
            {2, "チャージ"},
            {3, "券購(磁気券購入)"},
            {4, "精算"},
            {5, "精算 (入場精算)"},
            {6, "窓出 (改札窓口処理)"},
            {7, "新規 (新規発行)"},
            {8, "控除 (窓口控除)"},
            {13, "バス (PiTaPa系)"},
            {15, "バス (IruCa系)"},
            {17, "再発 (再発行処理)"},
            {19, "支払 (新幹線利用)"},
            {20, "入A (入場時オートチャージ)"},
            {21, "出A (出場時オートチャージ)"},
            {31, "入金 (バスチャージ)"},
            {35, "券購 (バス路面電車企画券購入)"},
            {70, "物販"},
            {72, "特典 (特典チャージ)"},
            {73, "入金 (レジ入金)"},
            {74, "物販取消"},
            {75, "入物 (入場物販)"},
            {198, "物現 (現金併用物販)"},
            {203, "入物 (入場現金併用物販)"},
            {132, "精算 (他社精算)"},
            {133, "精算 (他社入場精算)"},
        };
    }
}

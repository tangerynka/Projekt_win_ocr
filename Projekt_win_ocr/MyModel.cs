using IronOcr;
using IronPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_win_ocr.MyModelNS
{
    class MyModel
    {
        public Dictionary<string, IronOcr.Languages.IOcrLanguagePack> languages = new Dictionary<string, IronOcr.Languages.IOcrLanguagePack>()
        {
            {"English", IronOcr.Languages.English.OcrLanguagePack },
            {"Korean", IronOcr.Languages.Korean.OcrLanguagePack },
            {"Polish", IronOcr.Languages.Polish.OcrLanguagePack }
        };
        public Dictionary<string, IronOcr.AdvancedOcr.OcrStrategy> strategies = new Dictionary<string, IronOcr.AdvancedOcr.OcrStrategy>()
        {
            {"Advanced", IronOcr.AdvancedOcr.OcrStrategy.Advanced },
            {"Fast", IronOcr.AdvancedOcr.OcrStrategy.Fast},
        };

        public string ConvertPDF(string path, bool adv, bool bsc, bool _cleanBackgroundNoise, bool _enhanceContrast, bool _enhanceResolution, string language, string strategy, bool _detectWhiteTextOnDarkBackgrounds, bool _rotateAndStraighten, bool _readBarCodes)
        {
            if (bsc)
            {
                return ConvertBasic(path, language);
            }
            else if (adv)
            {
                return ConvertAdvanced(path, _cleanBackgroundNoise, _enhanceContrast, _enhanceResolution, language, strategy, _detectWhiteTextOnDarkBackgrounds, _rotateAndStraighten,_readBarCodes);
            }
            else return null;
            
        }

        private string ConvertAdvanced(string path, bool _cleanBackgroundNoise, bool _enhanceContrast, bool _enhanceResolution, string language, string strategy, bool _detectWhiteTextOnDarkBackgrounds, bool _rotateAndStraighten, bool _readBarCodes)
        {
            var Ocr = new IronOcr.AdvancedOcr()
            {
                CleanBackgroundNoise = _cleanBackgroundNoise,
                EnhanceContrast = _enhanceContrast,
                EnhanceResolution = _enhanceResolution,
                Language = languages[language],
                Strategy = strategies[strategy],
                ColorSpace = AdvancedOcr.OcrColorSpace.Color,
                DetectWhiteTextOnDarkBackgrounds = _detectWhiteTextOnDarkBackgrounds,
                InputImageType = AdvancedOcr.InputTypes.AutoDetect,
                RotateAndStraighten = _rotateAndStraighten,
                ReadBarCodes = _readBarCodes,
                ColorDepth = 4
            };
            var testDocument = path;
            var Result = Ocr.Read(testDocument);
            return Result.Text;
        }

        private string ConvertBasic(string path, string language)
        {
            var Ocr = new IronOcr.AutoOcr();
            Ocr.Language = languages[language];
            var testDocument = path;
            var Result = Ocr.Read(testDocument);

            return Result.Text;
        }

        internal void SavePDF(string outPath, string outFile, string ReadText)
        {
            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            // Render an HTML document or snippet as a string
            Renderer.RenderHtmlAsPdf(ReadText).SaveAs(outPath+outFile+".pdf");

        }
    }
}

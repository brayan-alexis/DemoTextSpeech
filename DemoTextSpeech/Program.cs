using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace DemoTextSpeech
{
    class Program
    {
        private static string suscriptionKey = "<Suscription-Key>"; // Ej. bf71a75ac84e4a355f4b3d64b1fefe32
        private static string serviceRegion = "<Service-Region>"; // Ej. centralus

        static async Task Main()
        {
            Console.WriteLine("Testing Speech to Text");
            await SynthesizeAudioToSpeakerAsync();
            await SynthesizeAudioToFileAsync();
            Console.ReadLine();
        }

        static async Task SynthesizeAudioToSpeakerAsync()
        {
            var config = SpeechConfig.FromSubscription(suscriptionKey, serviceRegion);
            config.SpeechSynthesisLanguage = "es-MX"; // Establece el idioma en español de México
            config.SpeechSynthesisVoiceName = "es-MX-DaliaNeural"; // Establece el acento en español de México
            using var synthesizer = new SpeechSynthesizer(config);
            //await synthesizer.SpeakTextAsync("Hello, I am testing the text to speech service at the moment.");
            await synthesizer.SpeakTextAsync("Hola, estoy probando el servicio de texto a voz en este momento.");
        }

        static async Task SynthesizeAudioToFileAsync()
        {
            var config = SpeechConfig.FromSubscription(suscriptionKey, serviceRegion);
            config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff24Khz16BitMonoPcm);

            using var synthesizer = new SpeechSynthesizer(config, null);

            var ssml = File.ReadAllText("ssml.xml");
            var resultssml = await synthesizer.SpeakSsmlAsync(ssml);

            using var stream = AudioDataStream.FromResult(resultssml);
            await stream.SaveToWaveFileAsync("output-audio-test.wav");
        }
    }
}

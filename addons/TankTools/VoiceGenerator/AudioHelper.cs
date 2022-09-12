using Amazon.Runtime;
using Amazon.Polly;
using Amazon.Polly.Model;
namespace VoiceGenerator
{
    public class AudioHelper
    {
        private readonly AmazonPollyClient polly;
        public AudioHelper()
        {
            var credentials = new BasicAWSCredentials("", "");

            polly = new AmazonPollyClient(credentials, Amazon.RegionEndpoint.EUWest1);
        }

        public async Task GenerateOgg(string text)
        {
            var response = await polly.SynthesizeSpeechAsync(new SynthesizeSpeechRequest
            {
                LanguageCode = LanguageCode.EnGB,
                OutputFormat = OutputFormat.Ogg_vorbis,
                TextType = TextType.Text,
                VoiceId = VoiceId.Amy,
                Engine = Engine.Standard
            });
        }

        private async void SaveFile(SynthesizeSpeechResponse response, string path)
        {
            Stream audioStream = response.AudioStream;
            using(MemoryStream ms = new MemoryStream())
            using(FileStream fs = new FileStream(path))
            {
                await audioStream.CopyToAsync(ms);
                byte[] buffer = ms.ToArray();
                await audioStream.ReadAsync(buffer);
                await fs.WriteAsync(buffer);
            }
        }
    }
}
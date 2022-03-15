using System;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using CliParser = CommandLine.Parser;
using YamlParser = YamlDotNet.Core.Parser;

namespace Yaml2Json
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var result = CliParser.Default.ParseArguments(args, typeof(CliArgs));
                await result.MapResult<CliArgs, Task>(a => Run(a), e => Task.FromResult(false));
            }
            catch (YamlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task Run(CliArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Target))
                args.Target = Path.ChangeExtension(args.Source, ".json");

            long count = 0;

            using (var reader = new StreamReader(args.Source))
            using (var writer = new StreamWriter(args.Target))
            {
                var deserializer = new DeserializerBuilder().Build();
                var parser = new YamlParser(reader);
                parser.Consume<StreamStart>();
                while (parser.Current is DocumentStart)
                {
                    object source = deserializer.Deserialize(parser);

                    if (source != null)
                    {
                        await writer.WriteLineAsync(JsonConvert.SerializeObject(source, Formatting.Indented));
                        count++;
                    }
                }
                parser.Consume<StreamEnd>();
            }

            Console.WriteLine($"Wrote {count} line(s) to {args.Target}");
        }
    }

    [Verb("convert")]
    public sealed class CliArgs
    {
        [Value(0, Required = true)]
        public string Source { get; set; }

        [Value(1, Required = false)]
        public string Target { get; set; }
    }
}
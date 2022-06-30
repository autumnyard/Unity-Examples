using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;
using System;

namespace AutumnYard.Tools.Benchmarking
{
    public struct Benchmark : IBenchmark
    {
        private readonly string _name;
        private readonly string _systemInfo;
        private readonly string _buildInfo;
        private readonly DateTime _date;

        private List<int> _frames;
        private List<float> _times;
        private List<string> _messages;

        public Benchmark(string name)
        {
            _name = name;
            _frames = new List<int>();
            _times = new List<float>();
            _messages = new List<string>();
            _systemInfo = UnityEngine.Application.platform.ToString();
            _buildInfo = UnityEngine.Application.isEditor ? "Editor" : "Player";
            _date = DateTime.Now;
        }

        public string Tick(in int frame, in float time, in string message)
        {
            _frames.Add(frame);
            _times.Add(time);
            _messages.Add(message);

            if (_frames.Count > 1)
            {
                return $"Tick >>  {message} --- diff: {frame - _frames[_frames.Count - 2]}s (abs: {time:f3}s ({frame} frames))";
            }
            else
            {
                return $"Tick >>  {message} --- diff: 0s (abs: {time:f3}s ({frame} frames))";
            }
        }

        public string GetLast()
        {
            int length = _frames.Count - 1;

            if (_frames.Count > 1)
            {
                return $"Tick >>  {_messages[length]} --- diff: {_times[length] - _times[length - 1]}s (abs: {_times[length]:f3}s ({_frames[length]} frames))";
            }
            else
            {
                return $"Tick >>  {_messages[length]} --- diff: 0s (abs: {_times[length]:f3}s ({_frames[length]} frames))";
            }

            //return $"{_messages[length]}: Tick >> {_times[length]:f3}s ({_frames[length]} frames)";
        }

        public string GetAll(in Format format, in bool printSystemInfo)
        {

            switch (format)
            {
                case Format.Log:
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine($"Results of benchmark");

                        sb.AppendLine();
                        sb.AppendLine($"=========");
                        sb.AppendLine($"   {_name}");
                        sb.AppendLine();

                        if (printSystemInfo)
                        {
                            sb.AppendLine($" --- Benchmark Info ---");
                            sb.AppendLine($"System: {_systemInfo}");
                            sb.AppendLine($"Build: {_buildInfo}");
                            sb.AppendLine($"Date: {_date.ToString("yyyy-MM-dd")}");
                            sb.AppendLine($"Time of creation: {_date.ToString("HH:mm:ss")}");
                            sb.AppendLine($"Name: {_name}");
#if UNITY_2021_2_OR_NEWER
                            sb.AppendLine($"Last tick: {_times[^1]}");
#else
                            sb.AppendLine($"Last tick: {_times[_times.Count - 1]}");
#endif
                        }

                        sb.AppendLine();

                        sb.AppendLine($" --- Results ---");
                        for (int i = 0; i < _frames.Count; i++)
                        {
                            float diff = i > 0 ? _times[i] - _times[i - 1] : 0f;

                            if (diff == 0f)
                            {
                                sb.AppendLine($"Tick >>  {_messages[i]}");
                            }
                            else
                            {
                                sb.AppendLine($"Tick >>  {_messages[i]} --- diff: {diff:f3}s \t abs: {_times[i]:f3}s ({_frames[i]} frames)");
                            }
                        }
                        sb.Append(" =========");
                        return sb.ToString();
                    }

                case Format.CSV:
                    {
                        var sb = new StringBuilder();

                        if (printSystemInfo)
                        {
                            sb.Append("Name,System,Build,Date,Time,Last Tick,");
                        }

                        sb.Append("Diff Time,Abs Time,Frame,Message");

                        for (int i = 0; i < _frames.Count; i++)
                        {
                            float diff = i > 0 ? _times[i] - _times[i - 1] : 0f;
                            sb.Append('\n');

                            if (printSystemInfo)
                            {
                                sb.Append(_name).Append(',')
                                    .Append(_systemInfo).Append(',')
                                    .Append(_buildInfo).Append(',')
                                    .Append(_date.ToString("yyyy-MM-dd")).Append(',')
                                    .Append(_date.ToString("HH:mm:ss")).Append(',')
#if UNITY_2021_2_OR_NEWER
                                    .Append(_times[^1].ToString("F4", CultureInfo.InvariantCulture)).Append(',');
#else
                                    .Append(_times[_times.Count - 1].ToString("F4", CultureInfo.InvariantCulture)).Append(',');
#endif
                            }

                            sb.Append(diff.ToString("F4", CultureInfo.InvariantCulture)).Append(',')
                                .Append(_times[i].ToString("F4", CultureInfo.InvariantCulture)).Append(',')
                                .Append(_frames[i].ToString()).Append(',')
                                .Append(_messages[i]);
                        }

                        return sb.ToString();
                    }

                default: return string.Empty;
            }
        }

        public void Export(in Format format, in bool printSystemInfo)
        {
#if UNITY_SWITCH
            string fileName = $"benchmark-{_date.ToString("yyyy-MM-dd-HH-mm-ss")}";
#else
            string fileName = $"{_name}-{_systemInfo}-{_buildInfo}-{_date.ToString("yyyy-MM-dd-HH-mm-ss")}";
#endif
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

#if UNITY_STANDALONE_WIN
            if (!Directory.Exists("Benchmarks/"))
                Directory.CreateDirectory("Benchmarks/");

            File.WriteAllText($"Benchmarks/{fileName}.csv", GetAll(format, printSystemInfo));
#elif UNITY_SWITCH && UNITY_EDITOR
            if (!Directory.Exists("Benchmarks/"))
                Directory.CreateDirectory("Benchmarks/");

            File.WriteAllText($"Benchmarks/{fileName}.csv", GetAll(format, printSystemInfo));
#elif UNITY_SWITCH && !UNITY_EDITOR
            string text = GetAll(format, printSystemInfo);
            string filePath = IO.File.CalculateFilePath_Persistent("Benchmarks", $"{fileName}", ".csv");
            ThirdParty.NintendoSwitch.File.WriteStringToFile(text, filePath);
#else
#endif

        }
    }
}

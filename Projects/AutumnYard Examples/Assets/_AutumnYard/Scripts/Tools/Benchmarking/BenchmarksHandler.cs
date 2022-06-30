using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AutumnYard.Tools.Benchmarking
{
    public sealed partial class BenchmarksHandler
    {
        private IDictionary<int, IBenchmark> _benchmarks;
        private ILogger _logger;

        public BenchmarksHandler()
        {
            _benchmarks = new Dictionary<int, IBenchmark>();
            _logger = Debug.unityLogger;
        }
        public BenchmarksHandler(ILogger customLogger)
        {
            _benchmarks = new Dictionary<int, IBenchmark>();
            _logger = customLogger;
        }

        /// <summary> Perform a benchmark that iterates over the provided array. </summary>
        /// <typeparam name="T">Type to pass to the action.</typeparam>
        /// <param name="array">Array of elements to iterate over.</param>
        /// <param name="action">Action to benchmark.</param>
        /// <param name="name">Name for the benchmark.</param>
        /// <param name="messageBegin">Message for the tick before beginning an iteration.</param>
        /// <param name="messageFinish">Message for the tick after finishing an iteration.</param>
        /// <returns></returns>
        public IEnumerator PerformIterationBenchmark<T>(
            T[] array, Func<T, IEnumerator> action,
            string name, string messageBegin, string messageFinish)
        {
            int id = Create(name);
            Tick(id, $"{messageBegin}");

            foreach (var element in array)
            {
                //Tick(id, $"{messageBegin} {element}");
                yield return action(element);
                Tick(id, $"{messageFinish} {element}");
                //_logger.Log(GetLast(id));
            }

            PrintRecord(id, Benchmarking.Format.Log, true);
            PrintRecord(id, Benchmarking.Format.CSV, true);
            ExportRecord(id, Benchmarking.Format.CSV, true);

            Remove(id);
        }

        /// <summary> Create a new Benchmark, set a name and get the identifier. </summary>
        /// <param name="name">Name for the benchmark.</param>
        /// <returns>Unique identifier to perform further operations.</returns>
        public int Create(string name)
        {
            int index = 0;
            while (_benchmarks.ContainsKey(index))
            {
                index++;
            }

            _benchmarks.Add(index, new Benchmark(name));
            _logger.Log($"[Benchmark] {name}: Created (on index {index})");
            return index;
        }

        /// <summary> Set a milestone in the benchmark. </summary>
        /// <param name="id">Unique identifier of the benchmark.</param>
        /// <param name="message">Set a custom message to explain this tick.</param>
        public void Tick(int id, string message)
        {
            if (!_benchmarks.ContainsKey(id))
                return;

            _benchmarks[id].Tick(Time.frameCount, Time.time, message);
            _logger.Log($"[Benchmark] {_benchmarks[id].GetLast()}");
        }

        /// <summary> Get last tick. </summary>
        /// <param name="id"></param>
        /// <returns>Formatted information about the last tick.</returns>
        public string GetLast(int id)
        {
            if (_benchmarks.ContainsKey(id))
                return _benchmarks[id].GetLast();

            return string.Empty;
        }

        public void PrintRecord(int id, Benchmarking.Format format, bool printSystemData)
        {
            if (!_benchmarks.ContainsKey(id))
                return;

            string text = _benchmarks[id].GetAll(format, printSystemData);


            _logger.Log(text);
        }

        public void ExportRecord(int id, Benchmarking.Format format, bool printSystemData)
        {
            if (!_benchmarks.ContainsKey(id))
                return;

            _benchmarks[id].Export(format, printSystemData);
        }

        /// <summary> Get all ticks up until now. </summary>
        /// <param name="id"></param>
        /// <returns>Formatted information about all accumulated ticks.</returns>
        public string GetAll(int id)
        {
            if (_benchmarks.ContainsKey(id))
                return _benchmarks[id].GetAll(Benchmarking.Format.Log, false);

            return string.Empty;
        }

        /// <summary> Remove the benchmark corresponding to this identifier. </summary>
        /// <param name="id">Identifier of the benchmark to remove.</param>
        public void Remove(int id)
        {
            _benchmarks.Remove(id);
        }
    }
}

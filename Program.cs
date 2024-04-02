using BenchmarkDotNet.Running;
using NaiveECS.Benchmarks;
using NaiveECS.Example;
using NaiveECS.Example.Roguelike;

var game = new ConsoleGame(new RoguelikeBoot());
// BenchmarkRunner.Run<MemoryBenchmarkerDemo>();


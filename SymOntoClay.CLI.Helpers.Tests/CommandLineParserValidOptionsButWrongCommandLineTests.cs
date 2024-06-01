using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options.TypeCheckers;

namespace SymOntoClay.CLI.Helpers.Tests
{
    public class CommandLineParserValidOptionsButWrongCommandLineTests
    {
        [Test]
        public void DuplicatedMutuallyExclusiveSet_WrongCommandLine_Fail()
        {
            var args = new List<string>()
            { 
                "help",
                "run"
            };

            var exception = Assert.Catch<DuplicatedMutuallyExclusiveOptionsSetException>(() => {
                var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Options 'help', 'run' cannot be used at the same time."));
        }

        [Test]
        public void DuplicatedMutuallyExclusiveSet_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
            {
                "help",
                "run"
            };

            var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Options 'help', 'run' cannot be used at the same time."));
        }

        [Test]
        public void RequredNoDefaultOptions_EmptyCommandLine_Fail()
        {
            var args = new List<string>();

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void RequredNoDefaultOptions_EmptyCommandLine_ErrorsList()
        {
            var args = new List<string>();

            var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSet(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void RequredNoDefaultOptions_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSetAndOptionalParams());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void RequredNoDefaultOptions_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var parser = new CommandLineParser(GetMinimalRequiredMutuallyExclusiveSetAndOptionalParams(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Required command line arguments must be entered."));
        }

        [Test]
        public void OneSingleValueOption_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(GetOneSingleValueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--input' must be a single value, but used as flag."));
        }

        [Test]
        public void OneSingleValueOption_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var parser = new CommandLineParser(GetOneSingleValueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--input' must be a single value, but used as flag."));
        }

        [Test]
        public void TwoSingleValueOptions_WrongCommandLine_Case1_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--o",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(GetTwoSingleValueOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--input' must be a single value, but used as flag."));
        }

        [Test]
        public void TwoSingleValueOptions_WrongCommandLine_Case1_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--o",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\"
                };

            var parser = new CommandLineParser(GetTwoSingleValueOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--input' must be a single value, but used as flag."));
        }

        [Test]
        public void TwoSingleValueOptions_WrongCommandLine_Case2_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(GetTwoSingleValueOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--output' must be a single value, but used as flag."));
        }

        [Test]
        public void TwoSingleValueOptions_WrongCommandLine_Case2_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o"
                };

            var parser = new CommandLineParser(GetTwoSingleValueOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--output' must be a single value, but used as flag."));
        }

        [Test]
        public void OnePositionedOption_ExtraValueInCommandLine_Case1_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

            var exception = Assert.Catch<UnknownValueException>(() => {
                var parser = new CommandLineParser(GetOneSingleValueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
        }

        [Test]
        public void OnePositionedOption_ExtraValueInCommandLine_Case1_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

            var parser = new CommandLineParser(GetOneSingleValueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
        }

        [Test]
        public void OnePositionedOption_ExtraValueInCommandLine_Case2_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

            var exception = Assert.Catch<UnknownValueException>(() => {
                var parser = new CommandLineParser(GetOneSingleValueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
        }

        [Test]
        public void OnePositionedOption_ExtraValueInCommandLine_Case2_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

            var parser = new CommandLineParser(GetOneSingleValueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(2));
            Assert.That(result.Errors[0], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
            Assert.That(result.Errors[1], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\' in 3 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case1_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

            var exception = Assert.Catch<UnknownValueException>(() => {
                var parser = new CommandLineParser(GetTwoSingleValueOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case1_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

            var parser = new CommandLineParser(GetTwoSingleValueOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case2_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

            var exception = Assert.Catch<UnknownValueException>(() => {
                var parser = new CommandLineParser(GetTwoSingleValueOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case2_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\"
                };

            var parser = new CommandLineParser(GetTwoSingleValueOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(2));
            Assert.That(result.Errors[0], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 2 position."));
            Assert.That(result.Errors[1], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\' in 3 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case3_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

            var exception = Assert.Catch<UnknownValueException>(() => {
                var parser = new CommandLineParser(GetTwoSingleValueOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 4 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case3_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\"
                };

            var parser = new CommandLineParser(GetTwoSingleValueOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 4 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case4_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

            var exception = Assert.Catch<UnknownValueException>(() => {
                var parser = new CommandLineParser(GetTwoSingleValueOptions());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 4 position."));
        }

        [Test]
        public void TwoPositionedOptions_ExtraValueInCommandLine_Case4_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_10_14_58_31\",
                    "--o",
                    @"c:\Users\SomeUser\source\repos\SymOntoClay\TestSandbox\bin\Debug\net7.0\MessagesLogsOutputDir\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\",
                    @"c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\"
                };

            var parser = new CommandLineParser(GetTwoSingleValueOptions(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(2));
            Assert.That(result.Errors[0], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\2024_03_11_14_58_31\' in 4 position."));
            Assert.That(result.Errors[1], Is.EqualTo(@"Unknown value 'c:\Users\SomeUser\AppData\Roaming\SymOntoClayAsset\NpcLogMessages\58_31\' in 5 position."));
        }

        [Test]
        public void OneListValueOption_FlagInTheEndOfCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(OneListValueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneListValueOption_FlagInTheEndOfCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var parser = new CommandLineParser(OneListValueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneListValueOption_FlagInTheMiddleOfCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(OneListValueOptionAndNextSingleValueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneListValueOption_FlagInTheMiddleOfCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

            var parser = new CommandLineParser(OneListValueOptionAndNextSingleValueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneSingleValueOrListOption_FlagInTheEndOfCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(OneSingleValueOrListOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneSingleValueOrListOption_FlagInTheEndOfCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input"
                };

            var parser = new CommandLineParser(OneSingleValueOrListOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneSingleValueOrListOption_FlagInTheMiddleOfCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

            var exception = Assert.Catch<ValueException>(() => {
                var parser = new CommandLineParser(OneSingleValueOrListOptionAndNextSingleValueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneSingleValueOrListOption_FlagInTheMiddleOfCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--input",
                    "--output",
                    "someValue"
                };

            var parser = new CommandLineParser(OneSingleValueOrListOptionAndNextSingleValueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("'--input' must be a single value or list of values, but used as flag."));
        }

        [Test]
        public void OneRequiredOptionAndOneNonRequiredOption_OnlyNonRequiredOptionInCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(OneRequiredOptionAndOneNonRequiredOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Required command line argument '--input' must be entered."));
        }

        [Test]
        public void OneRequiredOptionAndOneNonRequiredOption_OnlyNonRequiredOptionInCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var parser = new CommandLineParser(OneRequiredOptionAndOneNonRequiredOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Required command line argument '--input' must be entered."));
        }

        [Test]
        public void OneRequiredNamedGroupAndOneNonRequiredOption_OnlyNonRequiredOptionInCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(OneRequiredNamedGroupAndOneNonRequiredOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Required command line argument 'new' must be entered."));
        }

        [Test]
        public void OneRequiredNamedGroupAndOneNonRequiredOption_OnlyNonRequiredOptionInCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "-nologo"
                };

            var parser = new CommandLineParser(OneRequiredNamedGroupAndOneNonRequiredOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Required command line argument 'new' must be entered."));
        }

        [Test]
        public void OneUniqueOption_DuplicatedUniqueOptionInCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "-nologo",
                    "-nologo"
                };

            var exception = Assert.Catch<UniqueOptionException>(() => {
                var parser = new CommandLineParser(OneUniqueOption());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Option '-nologo' must be unique."));
        }

        [Test]
        public void OneUniqueOption_DuplicatedUniqueOptionInCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "-nologo",
                    "-nologo"
                };

            var parser = new CommandLineParser(OneUniqueOption(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Option '-nologo' must be unique."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithEnumCheckerAndWithTypeCheckErrorMessage_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var exception = Assert.Catch<TypeCheckingException>(() => {
                var parser = new CommandLineParser(OneSingleValuePositionedOptionWithEnumCheckerAndWithTypeCheckErrorMessage());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Unknown target framework 'Cat'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithEnumCheckerAndWithTypeCheckErrorMessage_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithEnumCheckerAndWithTypeCheckErrorMessage(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Unknown target framework 'Cat'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithEnumCheckerAndWithoutTypeCheckErrorMessage_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var exception = Assert.Catch<TypeCheckingException>(() => {
                var parser = new CommandLineParser(OneSingleValuePositionedOptionWithEnumCheckerAndWithoutTypeCheckErrorMessage());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Can not convert value 'Cat' to type 'TestEnum'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithEnumCheckerAndWithoutTypeCheckErrorMessage_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithEnumCheckerAndWithoutTypeCheckErrorMessage(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Can not convert value 'Cat' to type 'TestEnum'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithIntCheckerAndWithTypeCheckErrorMessage_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var exception = Assert.Catch<TypeCheckingException>(() => {
                var parser = new CommandLineParser(OneSingleValuePositionedOptionWithIntCheckerAndWithTypeCheckErrorMessage());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Unknown port 'Cat'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithIntCheckerAndWithTypeCheckErrorMessage_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithIntCheckerAndWithTypeCheckErrorMessage(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Unknown port 'Cat'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithIntCheckerAndWithoutTypeCheckErrorMessage_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var exception = Assert.Catch<TypeCheckingException>(() => {
                var parser = new CommandLineParser(OneSingleValuePositionedOptionWithIntCheckerAndWithoutTypeCheckErrorMessage());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Can not convert value 'Cat' to type 'Int32'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithIntCheckerAndWithoutTypeCheckErrorMessage_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithIntCheckerAndWithoutTypeCheckErrorMessage(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Can not convert value 'Cat' to type 'Int32'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithVersionCheckerAndWithTypeCheckErrorMessage_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var exception = Assert.Catch<TypeCheckingException>(() => {
                var parser = new CommandLineParser(OneSingleValuePositionedOptionWithVersionCheckerAndWithTypeCheckErrorMessage());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Unknown version 'Cat'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithVersionCheckerAndWithTypeCheckErrorMessage_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithVersionCheckerAndWithTypeCheckErrorMessage(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Unknown version 'Cat'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithVersionCheckerAndWithoutTypeCheckErrorMessage_WrongCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var exception = Assert.Catch<TypeCheckingException>(() => {
                var parser = new CommandLineParser(OneSingleValuePositionedOptionWithVersionCheckerAndWithoutTypeCheckErrorMessage());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Can not convert value 'Cat' to type 'Version'."));
        }

        [Test]
        public void OneSingleValuePositionedOptionWithVersionCheckerAndWithoutTypeCheckErrorMessage_WrongCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "Cat"
                };

            var parser = new CommandLineParser(OneSingleValuePositionedOptionWithVersionCheckerAndWithoutTypeCheckErrorMessage(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Can not convert value 'Cat' to type 'Version'."));
        }

        [Test]
        public void OneOptionWithRequires_AbsentRequiredParameterInCommandLine_Fail()
        {
            var args = new List<string>()
                {
                    "--abs-url"
                };

            var exception = Assert.Catch<RequiredOptionException>(() => {
                var parser = new CommandLineParser(Requires());
                parser.Parse(args.ToArray());
            });

            Assert.That(exception.Message, Is.EqualTo("Option '--html' is requied for '--abs-url'."));
        }

        [Test]
        public void OneOptionWithRequires_AbsentRequiredParameterInCommandLine_ErrorsList()
        {
            var args = new List<string>()
                {
                    "--abs-url"
                };

            var parser = new CommandLineParser(Requires(), true);
            var result = parser.Parse(args.ToArray());

            Assert.NotNull(result);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors[0], Is.EqualTo("Option '--html' is requied for '--abs-url'."));
        }

        private List<BaseCommandLineArgument> GetMinimalRequiredMutuallyExclusiveSet()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                        IsRequired = true,
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "help",
                                Aliases = new List<string>
                                {
                                    "h"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                            },
                            new CommandLineArgument()
                            {
                                Name = "run",
                                Aliases = new List<string>
                                {
                                    "r"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                            }
                        }
                    }
                };
        }

        private List<BaseCommandLineArgument> GetMinimalRequiredMutuallyExclusiveSetAndOptionalParams()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineMutuallyExclusiveSet()
                    {
                        IsRequired = true,
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "help",
                                Aliases = new List<string>
                                {
                                    "h"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                            },
                            new CommandLineArgument()
                            {
                                Name = "run",
                                Aliases = new List<string>
                                {
                                    "r"
                                },
                                Kind = KindOfCommandLineArgument.Flag,
                            }
                        }
                    },
                    new CommandLineGroup()
                    {
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineArgument()
                            {
                                Name = "-nologo",
                                Kind = KindOfCommandLineArgument.Flag
                            },
                            new CommandLineArgument()
                            {
                                Name = "-timeout",
                                Kind = KindOfCommandLineArgument.SingleValue
                            },
                            new CommandLineArgument()
                            {
                                Name = "-nlp",
                                Kind = KindOfCommandLineArgument.Flag
                            }
                        }
                    }
                };
        }

        private List<BaseCommandLineArgument> GetOneSingleValueOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.SingleValue,
                        Index = 0
                    }
                };
        }

        private List<BaseCommandLineArgument> GetTwoSingleValueOptions()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.SingleValue,
                        Index = 0
                    },
                    new CommandLineArgument
                    {
                        Name = "--output",
                        Aliases = new List<string>
                        {
                            "--o"
                        },
                        Kind = KindOfCommandLineArgument.SingleValue
                    }
                };
        }

        private List<BaseCommandLineArgument> OneListValueOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.List
                    }
                };
        }

        private List<BaseCommandLineArgument> OneListValueOptionAndNextSingleValueOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.List
                    },
                    new CommandLineArgument
                    {
                        Name = "--output",
                        Aliases = new List<string>
                        {
                            "--o"
                        },
                        Kind = KindOfCommandLineArgument.SingleValue
                    }
                };
        }

        private List<BaseCommandLineArgument> OneSingleValueOrListOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.SingleValueOrList
                    }
                };
        }

        private List<BaseCommandLineArgument> OneSingleValueOrListOptionAndNextSingleValueOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.SingleValueOrList
                    },
                    new CommandLineArgument
                    {
                        Name = "--output",
                        Aliases = new List<string>
                        {
                            "--o"
                        },
                        Kind = KindOfCommandLineArgument.SingleValue
                    }
                };
        }

        private List<BaseCommandLineArgument> OneRequiredOptionAndOneNonRequiredOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "--input",
                        Aliases = new List<string>()
                        {
                            "--i"
                        },
                        Kind = KindOfCommandLineArgument.SingleValue,
                        IsRequired = true
                    },
                    new CommandLineArgument()
                    {
                        Name = "-nologo",
                        Kind = KindOfCommandLineArgument.Flag
                    }
                };
        }

        private List<BaseCommandLineArgument> OneRequiredNamedGroupAndOneNonRequiredOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineNamedGroup()
                    {
                        Name = "new",
                        Aliases = new List<string>
                        {
                            "n"
                        },
                        IsRequired = true,
                        SubItems = new List<BaseCommandLineArgument>
                        {
                            new CommandLineMutuallyExclusiveSet()
                            {
                                IsRequired = true,
                                SubItems = new List<BaseCommandLineArgument>
                                {
                                    new CommandLineArgument()
                                    {
                                        Target = "NPCName",
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument()
                                    {
                                        Target = "NPCName",
                                        Name = "-npc",
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument()
                                    {
                                        Name = "-thing",
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument()
                                    {
                                        Name = "-world",
                                        Aliases = new List<string>()
                                        {
                                            "-w"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument()
                                    {
                                        Name = "-lib",
                                        Aliases = new List<string>()
                                        {
                                            "-l"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument()
                                    {
                                        Name = "-nav",
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    },
                                    new CommandLineArgument()
                                    {
                                        Name = "-player",
                                        Aliases = new List<string>()
                                        {
                                            "-p"
                                        },
                                        Kind = KindOfCommandLineArgument.SingleValue
                                    }
                                }
                            }
                        }
                    },
                    new CommandLineArgument()
                    {
                        Name = "-nologo",
                        Kind = KindOfCommandLineArgument.Flag
                    }
                };
        }

        private List<BaseCommandLineArgument> OneUniqueOption()
        {
            return new List<BaseCommandLineArgument>()
                {
                    new CommandLineArgument()
                    {
                        Name = "-nologo",
                        Kind = KindOfCommandLineArgument.Flag,
                        IsUnique = true
                    }
                };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithEnumCheckerAndWithTypeCheckErrorMessage()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "TargetFramework",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new EnumChecker<TestEnum>(),
                    TypeCheckErrorMessage = "Unknown target framework"
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithEnumCheckerAndWithoutTypeCheckErrorMessage()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "TargetFramework",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new EnumChecker<TestEnum>()
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithIntCheckerAndWithTypeCheckErrorMessage()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Port",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new IntChecker(),
                    TypeCheckErrorMessage = "Unknown port"
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithIntCheckerAndWithoutTypeCheckErrorMessage()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Port",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new IntChecker()
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithVersionCheckerAndWithTypeCheckErrorMessage()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Version",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new VersionChecker(),
                    TypeCheckErrorMessage = "Unknown version"
                }
            };
        }

        private List<BaseCommandLineArgument> OneSingleValuePositionedOptionWithVersionCheckerAndWithoutTypeCheckErrorMessage()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument()
                {
                    Target = "Version",
                    Kind = KindOfCommandLineArgument.SingleValue,
                    Index = 0,
                    TypeChecker = new VersionChecker()
                }
            };
        }

        private List<BaseCommandLineArgument> Requires()
        {
            return new List<BaseCommandLineArgument>()
            {
                new CommandLineArgument
                {
                    Name = "--html",
                    Kind = KindOfCommandLineArgument.Flag
                },
                new CommandLineArgument
                {
                    Name = "--abs-url",
                    Kind = KindOfCommandLineArgument.Flag,
                    Requires = new List<string>
                    {
                        "--html"
                    }
                }
            };
        }
    }
}

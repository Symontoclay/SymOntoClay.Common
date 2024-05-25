using SymOntoClay.CLI.Helpers.CommandLineParsing.Exceptions;
using SymOntoClay.CLI.Helpers.CommandLineParsing;
using SymOntoClay.CLI.Helpers.CommandLineParsing.Options;

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
    }
}

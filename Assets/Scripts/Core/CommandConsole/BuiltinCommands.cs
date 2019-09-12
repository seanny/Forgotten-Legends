using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Core.Camera;
using UnityEngine;

namespace Core.CommandConsole
{
    public static class BuiltinCommands
    {
        [RegisterCommand(Help = "Clear the command console", MaxArgCount = 0)]
        static void CommandClear(CommandArg[] args)
        {
            Terminal.Buffer.Clear();
        }

        [RegisterCommand(Help = "Display help information about a command", MaxArgCount = 1)]
        static void CommandHelp(CommandArg[] args)
        {
            if (args.Length == 0)
            {
                foreach (var command in Terminal.Shell.Commands)
                {
                    Terminal.Log("{0}: {1}", command.Key.PadRight(16), command.Value.help);
                }
                return;
            }

            string command_name = args[0].String.ToUpper();

            if (!Terminal.Shell.Commands.ContainsKey(command_name))
            {
                Terminal.Shell.IssueErrorMessage("Command {0} could not be found.", command_name);
                return;
            }

            var info = Terminal.Shell.Commands[command_name];

            if (info.help == null)
            {
                Terminal.Log("{0} does not provide any help documentation.", command_name);
            }
            else if (info.hint == null)
            {
                Terminal.Log(info.help);
            }
            else
            {
                Terminal.Log("{0}\nUsage: {1}", info.help, info.hint);
            }
        }

        [RegisterCommand(Help = "Time the execution of a command", MinArgCount = 1)]
        static void CommandTime(CommandArg[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            Terminal.Shell.RunCommand(JoinArguments(args));

            sw.Stop();
            Terminal.Log("Time: {0}ms", (double)sw.ElapsedTicks / 10000);
        }

        [RegisterCommand(Help = "Output message")]
        static void CommandPrint(CommandArg[] args)
        {
            Terminal.Log(JoinArguments(args));
        }

        [RegisterCommand(Help = "Output the stack trace of the previous message", MaxArgCount = 0)]
        static void CommandTrace(CommandArg[] args)
        {
#if UNITY_DEVELOPMENT || UNITY_EDITOR
            int log_count = Terminal.Buffer.Logs.Count;

            if (log_count - 2 < 0)
            {
                Terminal.Log("Nothing to trace.");
                return;
            }

            var log_item = Terminal.Buffer.Logs[log_count - 2];

            if (log_item.stack_trace == "")
            {
                Terminal.Log("{0} (no trace)", log_item.message);
            }
            else
            {
                Terminal.Log(log_item.stack_trace);
            }
#else
            Terminal.Log("Nothing to trace.");
#endif
        }

        [RegisterCommand(Help = "List all variables or set a variable value")]
        static void CommandSet(CommandArg[] args)
        {
            if (args.Length == 0)
            {
                foreach (var kv in Terminal.Shell.Variables)
                {
                    Terminal.Log("{0}: {1}", kv.Key.PadRight(16), kv.Value);
                }
                return;
            }

            string variable_name = args[0].String;

            if (variable_name[0] == '$')
            {
                Terminal.Log(TerminalLogType.Warning, "Warning: Variable name starts with '$', '${0}'.", variable_name);
            }

            Terminal.Shell.SetVariable(variable_name, JoinArguments(args, 1));
        }

        [RegisterCommand(Help = "List All GameObjects")]
        static void CommandListGameObjects(CommandArg[] args)
        {
            int maxCount = 0;
            string ignore = String.Empty;
            if (args.Length != 0)
            {
                maxCount = args[0].Int;
                ignore = args[1].String.ToString();
                if (ignore.Length < 1)
                {
                    ignore = string.Empty;
                }
            }
            GameObject[] activeGameObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            
            for(int i = 0; i < activeGameObjects.Length; i++)
            {
                if (activeGameObjects[i].name.Contains(ignore))
                {
                    continue;
                }
                if (activeGameObjects[i].activeInHierarchy)
                {
                    Terminal.Log(TerminalLogType.Message, $"#{i}: {activeGameObjects[i].name} ({activeGameObjects[i].tag})");
                }

                if (maxCount > 0 && i >= maxCount)
                {
                    break;
                }
            }
        }

        [RegisterCommand(Help = "Set Actor Stat")]
        static void CommandSetActorStat(CommandArg[] args)
        {
            // Usage: SetActorStat [Actor Name] [Stat Name] [Value]
            // SetActorStat Player Intelligence 100

            if (args.Length < 3)
            {
                return;
            }
            
            string actorName = args[0].String;
            string statName = args[1].String;
            int value = args[2].Int;

            Actor.Actor actorID = Actor.Actor.FindActor(actorName);
            if (actorID == null)
            {
                Terminal.Log(TerminalLogType.Error, $"SetActorValue: Actor Name invalid.");
                return;
            }

            if (actorID.SetActorStat(statName, value) == false)
            {
                Terminal.Log(TerminalLogType.Error, $"SetActorValue: Stat Name invalid.");
                return;
            }
        }

        [RegisterCommand(Help = "No operation")]
        static void CommandNoop(CommandArg[] args) { }

        [RegisterCommand(Help = "Quit running application", MaxArgCount = 0)]
        static void CommandQuit(CommandArg[] args)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        static string JoinArguments(CommandArg[] args, int start = 0)
        {
            var sb = new StringBuilder();
            int arg_length = args.Length;

            for (int i = start; i < arg_length; i++)
            {
                sb.Append(args[i].String);

                if (i < arg_length - 1)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }
    }
}
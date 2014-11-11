//
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

//
namespace AoikWinWhich
{
    class AoikWinWhich
    {
        static List<String> find_executable(String prog)
        {
            // 8f1kRCu
            var env_var_PATHEXT = Environment.GetEnvironmentVariable("PATHEXT");
            /// can be null

            // 6qhHTHF
            // split into a list of extensions
            var ext_s = (env_var_PATHEXT == null)
                ? new List<String>()
                : new List<String>(env_var_PATHEXT.Split(Path.PathSeparator));

            // 2pGJrMW
            // strip
            ext_s = ext_s.Select(x => x.Trim()).ToList();

            // 2gqeHHl
            // remove empty
            ext_s = ext_s.Where(x => x != "").ToList();

            // 2zdGM8W
            // convert to lowercase
            ext_s = ext_s.Select(x => x.ToLower()).ToList();

            // 2fT8aRB
            // uniquify
            ext_s = ext_s.Distinct().ToList();

            // 4ysaQVN
            var env_var_PATH = Environment.GetEnvironmentVariable("PATH");
            /// can be null

            var dir_path_s = (env_var_PATH == null)
                ? new List<String>()
                : new List<String>(env_var_PATH.Split(Path.PathSeparator));

            // 5rT49zI
            // insert empty dir path to the beginning
            //
            // Empty dir handles the case that |prog| is a path, either relative or
            //  absolute. See code 7rO7NIN.
            dir_path_s.Insert(0, "");
            
            // 2klTv20
            // uniquify
            dir_path_s = dir_path_s.Distinct().ToList();
            
            //
            var prog_lc = prog.ToLower();

            var prog_has_ext = ext_s.Any(ext => prog_lc.EndsWith(ext));

            // 6bFwhbv
            var exe_path_s = new List<String>();

            foreach (var dir_path in dir_path_s)
            {
                // 7rO7NIN
                // synthesize a path with the dir and prog
                var path = (dir_path == "")
                    ? prog
                    : Path.Combine(dir_path, prog);
                
                // 6kZa5cq
                // assume the path has extension, check if it is an executable
                if (prog_has_ext && File.Exists(path))
                {
                    exe_path_s.Add(path);
                }

                // 2sJhhEV
                // assume the path has no extension
                foreach (var ext in ext_s)
                {
                    // 6k9X6GP
                    // synthesize a new path with the path and the executable extension
                    var path_plus_ext = path + ext;

                    // 6kabzQg
                    // check if it is an executable
                    if (File.Exists(path_plus_ext))
                    {
                        exe_path_s.Add(path_plus_ext);
                    }
                }
            }

            // 8swW6Av
            // uniquify
            exe_path_s = exe_path_s.Distinct().ToList();

            //
            return exe_path_s;
        }

        static void Main(String[] args)
        {
            // 9mlJlKg
            if (args.Length != 1)
            {
                // 7rOUXFo
                // print program usage
                Console.WriteLine(@"Usage: aoikwinwhich PROG");
                Console.WriteLine(@"");
                Console.WriteLine(@"#/ PROG can be either name or path");
                Console.WriteLine(@"aoikwinwhich notepad.exe");
                Console.WriteLine(@"aoikwinwhich C:\Windows\notepad.exe");
                Console.WriteLine(@"");
                Console.WriteLine(@"#/ PROG can be either absolute or relative");
                Console.WriteLine(@"aoikwinwhich C:\Windows\notepad.exe");
                Console.WriteLine(@"aoikwinwhich Windows\notepad.exe");
                Console.WriteLine(@"");
                Console.WriteLine(@"#/ PROG can be either with or without extension");
                Console.WriteLine(@"aoikwinwhich notepad.exe");
                Console.WriteLine(@"aoikwinwhich notepad");
                Console.WriteLine(@"aoikwinwhich C:\Windows\notepad.exe");
                Console.WriteLine(@"aoikwinwhich C:\Windows\notepad");

                // 3nqHnP7
                return;
            }

            // 9m5B08H
            // get name or path of a program from cmd arg
            var prog = args[0];

            // 8ulvPXM
            // find executables
            var path_s = find_executable(prog);

            // 5fWrcaF
            // has found none, exit
            if (path_s.Count == 0)
            {
                // 3uswpx0
                return;
            }

            // 9xPCWuS
            // has found some, output
            var txt = String.Join("\n", path_s);

            Console.WriteLine(txt);

            // 4s1yY1b
            return;
        }
    }
}

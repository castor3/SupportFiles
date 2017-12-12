﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SupportFiles
{
	public class Document
	{
		public static bool ReadFromFile(string path, out IEnumerable<string> linesFromFile)
		{
			linesFromFile = Enumerable.Empty<string>();
			try {
				linesFromFile = File.ReadLines(path);
				return true;
			}
			catch (Exception exc) when (exc is IOException ||
										exc is FileNotFoundException ||
										exc is DirectoryNotFoundException ||
										exc is UnauthorizedAccessException) {
				MessageBox.Show(exc.Message);
				return false;
			}
		}
		public static IEnumerable<string> YieldReturnLinesFromFile(string path)
		{
			if (File.Exists(path)) {
				foreach (var item in File.ReadLines(path))
					yield return item;
			}
		}
		/// <summary>
		/// Rewrites the entire file with the argument newFileContent
		/// </summary>
		public static bool WriteToFile(string path, string[] newFileContent)
		{
			try {
				File.WriteAllLines(path, newFileContent);
				return true;
			}
			catch (Exception exc) when (exc is IOException ||
										exc is FileNotFoundException ||
										exc is DirectoryNotFoundException ||
										exc is UnauthorizedAccessException) {
				MessageBox.Show(exc.Message);
				return false;
			}
		}
		/// <summary>
		/// Appends the string to the end of the file
		/// </summary>
		public static bool AppendToFile(string path, string stringToSave)
		{
			try {
				File.AppendAllText(path, stringToSave);
				return true;
			}
			catch (Exception exc) when (exc is IOException ||
										exc is FileNotFoundException ||
										exc is DirectoryNotFoundException ||
										exc is UnauthorizedAccessException) {
				MessageBox.Show(exc.Message);
				return false;
			}
		}
		public static bool WriteToLogFile(string path, string logMessage)
		{
			var fullMessage = string.Concat(DateTime.Now, " : ", logMessage);
			if (!AppendToFile(path, fullMessage)) return false;
			return true;
		}
	}
}
﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MobileApp.Services;
using Xamarin.Forms;
using System.IO;
using System.Linq;

[assembly: Dependency(typeof(MobileApp.Droid.FileWorker))]
namespace MobileApp.Droid
{
	public class FileWorker : IFileWorker
	{
		public Task DeleteAsync(string filename)
		{
			// удаляем файл
			File.Delete(GetFilePath(filename));
			return Task.FromResult(true);
		}

		public Task<bool> ExistsAsync(string filename)
		{
			// получаем путь к файлу
			string filepath = GetFilePath(filename);
			// существует ли файл
			bool exists = File.Exists(filepath);
			return Task<bool>.FromResult(exists);
		}

		public Task<IEnumerable<string>> GetFilesAsync()
		{
			// получаем все все файлы из папки
			IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
											select Path.GetFileName(filepath);
			return Task.FromResult(filenames);
		}

		public async Task<string> LoadTextAsync(string filename)
		{
			string filepath = GetFilePath(filename);
			using (StreamReader reader = File.OpenText(filepath))
			{
				return await reader.ReadToEndAsync();
			}
		}

		public async Task SaveTextAsync(string filename, string text)
		{
			string filepath = GetFilePath(filename);
			using (StreamWriter writer = File.CreateText(filepath))
			{
				await writer.WriteAsync(text);
			}
		}

		// вспомогательный метод для построения пути к файлу
		string GetFilePath(string filename) => Path.Combine(GetDocsPath(), filename);
		// получаем путь к папке MyDocuments
		string GetDocsPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		
		public void WriteStream(string filename, Stream streamIn)
		{
			string filePath = GetFilePath(filename);
			using (FileStream fs = File.Create(filePath))
			{
				streamIn.CopyTo(fs);
			}
		}

		public string GetLocalFolderPath() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
	}
}
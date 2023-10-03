﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NotesShokhov.Interfaces;
using NotesShokhov.Models;
using NotesShokhov.ViewModels;
using System.Diagnostics;

namespace NotesShokhov.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public HomeController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _noteRepository.GetAllNotesAsync();
            var sortNotes = notes.OrderByDescending(x => x.DateCreate);
            return View(sortNotes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(string title, string text)
        {
            var note = new Note() {Title = title, Text = text, DateCreate = DateTime.UtcNow};
            await _noteRepository.AddAsync(note);
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
﻿using ClubAdministration.Wpf.Common.Contracts;
using ClubAdministration.Wpf.ViewModels;
using ClubAdministration.Wpf.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ClubAdministration.Wpf.Common
{
  public class WindowController : IWindowController
  {
    private readonly Dictionary<BaseViewModel, Window> _windows
      = new Dictionary<BaseViewModel, Window>();

    public void ShowWindow(BaseViewModel viewModel, bool showAsDialog = false)
    {
      Window window = viewModel switch
      {
        // Wenn viewModel null ist -> ArgumentNullException
        null => throw new ArgumentNullException(nameof(viewModel)),

        MainViewModel _ => new MainWindow(),

        EditMemberViewModel _ => new EditMemberWindow();

        // default -> InvalidOperationException
        _ => throw new InvalidOperationException($"Unbekanntes ViewModel '{viewModel}'"),
      };

      _windows[viewModel] = window;

      window.DataContext = viewModel;

      if (showAsDialog)
      {
        window.ShowDialog();
      }
      else
      {
        window.Show();
      }
    }

    public void CloseWindow(BaseViewModel viewModel)
    {
      if (_windows.ContainsKey(viewModel))
      {
        Window window = _windows[viewModel];
        _windows.Remove(viewModel);
        window.Close();
      }
    }
  }
}
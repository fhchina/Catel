﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PleaseWaitService.cs" company="Catel development team">
//   Copyright (c) 2008 - 2014 Catel development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#if NET

namespace Catel.Services
{
    using System;
    using Catel.Windows;

    /// <summary>
    /// Please wait service to show a please wait window during background activities. This service uses the <see cref="PleaseWaitWindow"/>
    /// for the actual displaying of the please wait status to the user.
    /// </summary>
    public class PleaseWaitService : IPleaseWaitService
    {
        #region Fields
        private int _showCounter;
        #endregion

        #region IPleaseWaitService Members
        /// <summary>
        /// Shows the please wait window with the specified status text.
        /// </summary>
        /// <param name="status">
        /// The status. When the string is <c>null</c> or empty, the default please wait text will be used.
        /// </param>
        public virtual void Show(string status = "")
        {
            PleaseWaitHelper.Show(status);
        }

        /// <summary>
        /// Shows the please wait window with the specified status text and executes the work delegate (in a background thread). When the work is finished,
        /// the please wait window will be automatically closed.
        /// </summary>
        /// <param name="workDelegate">The work delegate.</param>
        /// <param name="status">The status.</param>
        public virtual void Show(PleaseWaitWorkDelegate workDelegate, string status = "")
        {
            PleaseWaitHelper.Show(workDelegate, null, status);
        }

        /// <summary>
        /// Updates the status text.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        public virtual void UpdateStatus(string status)
        {
            PleaseWaitHelper.UpdateStatus(status);
        }

        /// <summary>
        /// Updates the status and shows a progress bar with the specified status text. The percentage will be automatically calculated.
        /// <para>
        /// </para>
        /// The busy indicator will automatically hide when the <paramref name="totalItems" /> is larger than <paramref name="currentItem" />.
        /// <para>
        /// </para>
        /// When providing the <paramref name="statusFormat" />, it is possible to use <c>{0}</c> (represents current item) and
        /// <c>{1}</c> (represents total items).
        /// </summary>
        /// <param name="currentItem">The current item.</param>
        /// <param name="totalItems">The total items.</param>
        /// <param name="statusFormat">The status format. Can be empty, but not <c>null</c>.</param>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="currentItem" /> is smaller than zero.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="statusFormat" /> is <c>null</c>.</exception>
        public virtual void UpdateStatus(int currentItem, int totalItems, string statusFormat = "")
        {
            PleaseWaitHelper.UpdateStatus(statusFormat, currentItem, totalItems);
        }

        /// <summary>
        /// Hides this please wait window.
        /// </summary>
        public virtual void Hide()
        {
            PleaseWaitHelper.Hide();

            _showCounter = 0;
        }

        /// <summary>
        /// Increases the number of clients that show the please wait window. The implementing class is responsible for holding a counter
        /// internally which a call to this method will increase.
        /// <para>
        /// </para>
        /// As long as the internal counter is not zero (0), the please wait window will stay visible. To decrease the counter, make a
        /// call to <see cref="Pop" />.
        /// <para>
        /// </para>
        /// A call to <see cref="Show(string)" /> or one of its overloads will not increase the internal counter. A call to <see cref="Hide" />
        /// will reset the internal counter to zero (0) and thus hide the window.
        /// </summary>
        /// <param name="status">The status.</param>
        public virtual void Push(string status = "")
        {
            UpdateStatus(status);

            _showCounter++;

            if (_showCounter > 0)
            {
                Show(status);
            }
        }

        /// <summary>
        /// Decreases the number of clients that show the please wait window. The implementing class is responsible for holding a counter internally which a call to this method will decrease.
        /// <para>
        /// </para>
        /// As long as the internal counter is not zero (0), the please wait window will stay visible. To increase the counter, make a call to <see cref="Pop" />.
        /// <para>
        /// </para>
        /// A call to <see cref="Show(string)" /> or one of its overloads will not increase the internal counter. A call to <see cref="Hide" /> will reset the internal counter to zero (0) and thus hide the window.
        /// </summary>
        public virtual void Pop()
        {
            if (_showCounter > 0)
            {
                _showCounter--;
            }

            if (_showCounter <= 0)
            {
                Hide();
            }
        }
        #endregion
    }
}

#endif
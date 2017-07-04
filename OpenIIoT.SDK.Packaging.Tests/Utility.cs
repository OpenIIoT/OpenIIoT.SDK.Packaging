/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █   ███    █▄
      █   ███    ███
      █   ███    ███     ██     █   █        █      ██    ▄█   ▄
      █   ███    ███ ▀███████▄ ██  ██       ██  ▀███████▄ ██   █▄
      █   ███    ███     ██  ▀ ██▌ ██       ██▌     ██  ▀ ▀▀▀▀▀██
      █   ███    ███     ██    ██  ██       ██      ██    ▄█   ██
      █   ███    ███     ██    ██  ██▌    ▄ ██      ██    ██   ██
      █   ████████▀     ▄██▀   █   ████▄▄██ █      ▄██▀    █████
      █
      █       ███
      █   ▀█████████▄
      █      ▀███▀▀██    ▄█████   ▄█████     ██      ▄█████
      █       ███   ▀   ██   █    ██  ▀  ▀███████▄   ██  ▀
      █       ███      ▄██▄▄      ██         ██  ▀   ██
      █       ███     ▀▀██▀▀    ▀███████     ██    ▀███████
      █       ███       ██   █     ▄  ██     ██       ▄  ██
      █      ▄████▀     ███████  ▄████▀     ▄██▀    ▄████▀
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Unit tests for the Utility class.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The GNU Affero General Public License (GNU AGPL)
      █
      █  Copyright (C) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  This program is free software: you can redistribute it and/or modify
      █  it under the terms of the GNU Affero General Public License as published by
      █  the Free Software Foundation, either version 3 of the License, or
      █  (at your option) any later version.
      █
      █  This program is distributed in the hope that it will be useful,
      █  but WITHOUT ANY WARRANTY; without even the implied warranty of
      █  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
      █  GNU Affero General Public License for more details.
      █
      █  You should have received a copy of the GNU Affero General Public License
      █  along with this program.  If not, see <http://www.gnu.org/licenses/>.
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                            */

using System;
using System.IO;
using System.Text;
using Xunit;

namespace OpenIIoT.SDK.Packaging.Tests
{
    /// <summary>
    ///     Unit tests for the <see cref="Packaging.Utility"/> class.
    /// </summary>
    public class Utility
    {
        #region Public Methods

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.ComputeFileSHA512Hash(string)"/> method.
        /// </summary>
        [Fact]
        public void ComputeFileSHA512Hash()
        {
            string emptyHash = "CF83E1357EEFB8BDF1542850D66D8007D620E4050B5715DC83F4A921D36CE9CE47D0D13C5D85F2B0FF8318D2877EEC2F63B931BD47417A81A538327AF927DA3E";

            Uri codeBaseUri = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            string codeBasePath = Uri.UnescapeDataString(codeBaseUri.AbsolutePath);

            string dataDirectory = Path.Combine(Path.GetDirectoryName(codeBasePath), "Data");

            string hash = Packaging.Utility.ComputeFileSHA512Hash(Path.Combine(dataDirectory, "empty.txt"));

            Assert.Equal(emptyHash, hash);
        }

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.ComputeFileSHA512Hash(string)"/> method with a file which can not be found
        ///     on the local file system.
        /// </summary>
        [Fact]
        public void ComputeFileSHA512HashFileNotFound()
        {
            Exception ex = Record.Exception(() => Packaging.Utility.ComputeFileSHA512Hash(Guid.NewGuid().ToString()));

            Assert.NotNull(ex);
            Assert.IsType<FileNotFoundException>(ex);
        }

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.ComputeSHA512Hash(byte[])"/> method.
        /// </summary>
        [Fact]
        public void ComputeSHA512HashBytes()
        {
            string testHash = "EE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF";

            byte[] test = Encoding.ASCII.GetBytes("test");

            string hash = Packaging.Utility.ComputeSHA512Hash(test);

            Assert.Equal(testHash, hash);
        }

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.ComputeSHA512Hash(string)"/> method.
        /// </summary>
        [Fact]
        public void ComputeSHA512HashString()
        {
            string testHash = "EE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF";

            string hash = Packaging.Utility.ComputeSHA512Hash("test");

            Assert.Equal(testHash, hash);
        }

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.GetRelativePath(string, string)"/> method.
        /// </summary>
        [Fact]
        public void GetRelativePath()
        {
            string baseDir = Path.Combine("base", "subdir");
            string file = Path.Combine(baseDir, "subsubdir", "file.ext");

            string expected = Path.Combine("subsubdir", "file.ext");

            string relative = Packaging.Utility.GetRelativePath(baseDir, file);

            Assert.Equal(expected, relative);
        }

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.GetRelativePath(string, string)"/> method with a string ending with a slash.
        /// </summary>
        [Fact]
        public void GetRelativePathEndsWithSlash()
        {
            string baseDir = Path.Combine("base", "subdir");
            baseDir += Path.DirectorySeparatorChar.ToString();

            string file = Path.Combine(baseDir, "subsubdir", "file.ext");

            string expected = Path.Combine("subsubdir", "file.ext");

            string relative = Packaging.Utility.GetRelativePath(baseDir, file);

            Assert.Equal(expected, relative);
        }

        /// <summary>
        ///     Tests the <see cref="Packaging.Utility.ToJson(object)"/> method.
        /// </summary>
        [Fact]
        public void ToJson()
        {
        }

        #endregion Public Methods
    }
}
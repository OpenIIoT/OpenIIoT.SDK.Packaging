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
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Contains miscellaneous static methods.
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

using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace OpenIIoT.SDK.Packaging
{
    /// <summary>
    ///     Contains miscellaneous static methods.
    /// </summary>
    internal static class Utility
    {
        #region Public Methods

        /// <summary>
        ///     Computes and returns the SHA512 hash of the contents of the specified file.
        /// </summary>
        /// <param name="file">The file for which the SHA512 hash is to be computed.</param>
        /// <returns>The SHA512 hash of the specified file.</returns>
        public static string ComputeFileSHA512Hash(string file)
        {
            if (File.Exists(file))
            {
                return ComputeSHA512Hash(File.ReadAllBytes(file));
            }
            else
            {
                throw new FileNotFoundException($"The file {file} could not be found.");
            }
        }

        /// <summary>
        ///     Computes and returns the SHA512 hash of the specified string.
        /// </summary>
        /// <param name="content">The string for which the SHA512 hash is to be computed.</param>
        /// <returns>The SHA512 hash of the specified string.</returns>
        public static string ComputeSHA512Hash(string content)
        {
            return ComputeSHA512Hash(Encoding.ASCII.GetBytes(content));
        }

        /// <summary>
        ///     Computes and returns the SHA512 hash of the specified byte array.
        /// </summary>
        /// <param name="content">The byte array for which the SHA512 hash is to be computed.</param>
        /// <returns>The SHA512 hash of the specified byte array.</returns>
        public static string ComputeSHA512Hash(byte[] content)
        {
            byte[] hash;

            using (SHA512 sha512 = new SHA512Managed())
            {
                hash = sha512.ComputeHash(content);
            }

            StringBuilder stringBuilder = new StringBuilder(128);

            foreach (byte b in hash)
            {
                stringBuilder.Append(b.ToString("X2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Returns the path of the specified file, relative to the specified base directory.
        /// </summary>
        /// <param name="baseDirectory">The base directory of the relative file reference.</param>
        /// <param name="file">The file for which the relative path is to be returned.</param>
        /// <returns>The path of the specified file, relative to the specified base directory.</returns>
        public static string GetRelativePath(string baseDirectory, string file)
        {
            if (!baseDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                baseDirectory += Path.DirectorySeparatorChar;
            }

            return file.Replace(baseDirectory, string.Empty);
        }

        /// <summary>
        ///     Serializes and returns as json the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized object.</returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        #endregion Public Methods
    }
}
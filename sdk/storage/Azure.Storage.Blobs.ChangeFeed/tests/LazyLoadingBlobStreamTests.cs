// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Test;
using NUnit.Framework;

namespace Azure.Storage.Blobs.ChangeFeed.Tests
{
    public class LazyLoadingBlobStreamTests : ChangeFeedTestBase
    {
        public LazyLoadingBlobStreamTests(bool async)
            : base(async, null /* RecordedTestMode.Record /* to re-record */)
        {
        }

        /// <summary>
        /// Tests Read() with various sized Reads().
        /// </summary>
        [Test]
        public async Task ReadAsync()
        {
            // Arrange
            await using DisposingContainer test = await GetTestContainerAsync();

            // Arrange
            int length = Constants.KB;
            byte[] exectedData = GetRandomBuffer(length);
            BlobClient blobClient = InstrumentClient(test.Container.GetBlobClient(GetNewBlobName()));
            using (var stream = new MemoryStream(exectedData))
            {
                await blobClient.UploadAsync(stream);
            }
            LazyLoadingBlobStream lazyStream = new LazyLoadingBlobStream(blobClient, offset: 0, blockSize: 157);
            byte[] actualData = new byte[length];
            int offset = 0;

            // Act
            int count = 0;
            while (offset + count < length)
            {
                for (count = 6; count < 37; count += 6)
                {
                    await lazyStream.ReadAsync(actualData, offset, count);
                    offset += count;
                }
            }
            await lazyStream.ReadAsync(actualData, offset, length - offset);

            // Assert
            TestHelper.AssertSequenceEqual(exectedData, actualData);
        }

        /// <summary>
        /// Tests LazyBlobStream parameter validation.
        /// </summary>
        [Test]
        public async Task ReadAsync_InvalidParameterTests()
        {
            // Arrange
            BlobClient blobClient = new BlobClient(new Uri("https://www.doesntmatter.com"));
            LazyLoadingBlobStream lazyStream = new LazyLoadingBlobStream(blobClient, offset: 0, blockSize: Constants.KB);

            // Act
            await TestHelper.AssertExpectedExceptionAsync<ArgumentNullException>(
                lazyStream.ReadAsync(buffer: null, offset: 0, count: 10),
                e => Assert.AreEqual($"buffer cannot be null.{Environment.NewLine}Parameter name: buffer", e.Message));

            await TestHelper.AssertExpectedExceptionAsync<ArgumentOutOfRangeException>(
                lazyStream.ReadAsync(buffer: new byte[10], offset: -1, count: 10),
                e => Assert.AreEqual(
                    $"Specified argument was out of the range of valid values.{Environment.NewLine}Parameter name: offset cannot be less than 0.",
                    e.Message));

            await TestHelper.AssertExpectedExceptionAsync<ArgumentOutOfRangeException>(
                lazyStream.ReadAsync(buffer: new byte[10], offset: 11, count: 10),
                e => Assert.AreEqual(
                    $"Specified argument was out of the range of valid values.{Environment.NewLine}Parameter name: offset cannot exceed buffer length.",
                    e.Message));

            await TestHelper.AssertExpectedExceptionAsync<ArgumentOutOfRangeException>(
                lazyStream.ReadAsync(buffer: new byte[10], offset: 1, count: -1),
                e => Assert.AreEqual(
                    $"Specified argument was out of the range of valid values.{Environment.NewLine}Parameter name: count cannot be less than 0.",
                    e.Message));

            await TestHelper.AssertExpectedExceptionAsync<ArgumentOutOfRangeException>(
                lazyStream.ReadAsync(buffer: new byte[10], offset: 5, count: 15),
                e => Assert.AreEqual(
                    $"Specified argument was out of the range of valid values.{Environment.NewLine}Parameter name: offset + count cannot exceed buffer length.",
                    e.Message));
        }
    }
}

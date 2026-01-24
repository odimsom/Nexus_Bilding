/**
 * Helper service for handling profile image uploads.
 * In a real application, this would interact with a storage service (S3, Supabase Storage, etc.).
 * For now, we'll simulate the upload and return a mock URL or base64 string.
 */

export async function uploadProfileImage(file: File): Promise<string> {
  await new Promise((resolve) => setTimeout(resolve, 1000));

  if (!file.type.startsWith('image/')) {
    throw new Error('Invalid file type. Please upload an image.');
  }

  if (file.size > 5 * 1024 * 1024) {
    throw new Error('File size too large. Maximum size is 5MB.');
  }

  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onloadend = () => {
      resolve(reader.result as string);
    };
    reader.onerror = reject;
    reader.readAsDataURL(file);
  });
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ver03.DataBase.FTP
{
    public class FileManager
    {
        public string defDir = @"c:\DEL";
        public string carsDir = @"\cars";
        public string photoBufferDir = @"\buffer";
        public FTP ftpClient = new FTP();


        public FileManager()
        {
            if (!System.IO.File.Exists(defDir))
            {
                Directory.CreateDirectory(defDir);
                Directory.CreateDirectory(defDir + carsDir);
            }
            else if (!System.IO.File.Exists(defDir + carsDir))
            {
                Directory.CreateDirectory(defDir + carsDir);
            }
        }

        public void CreateCarFolder(int id)
        {
            Directory.CreateDirectory(defDir + carsDir + @"\" + id.ToString());
        }
        public void CreateCarBuffeFolder()
        {
            string dir = defDir + photoBufferDir;
            System.IO.Directory.CreateDirectory(dir);
        }
        public void DeleteCarBufferFolder()
        {
            string path = defDir + photoBufferDir;
            try
            {
                Directory.Delete(path, true);
            }
            catch { }
        }

        public void LoadCarPhotos(int id)
        {
            try
            {
                CreateCarFolder(id);
                string[] fullFileNames = ftpClient.getFileNamesFromDirectory("/cars/" + id.ToString()); // OK
                for (int i = 0; i < fullFileNames.Count(); i++)
                {
                    //System.Windows.MessageBox.Show(fullFileNames[i] + "\n" + fullFileNames[i].Split('/')[3]);
                    ftpClient.downloadFiles(fullFileNames[i], defDir + carsDir + @"\" + id.ToString() + @"\" + fullFileNames[i].Split('/')[3]); // WARNING [3]
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка загрузки фотографий для авто");
            }
        }
        public string[] GetPhotosByCar(int id)
        {
            try
            {
                return Directory.GetFiles(defDir + carsDir + @"\" + id.ToString() + @"\");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                return null;
            }

        }

        public void FTPCreateCarPhotoFolder(int id)
        {
            string defFolder = @"\cars\";
            ftpClient.createDirectory(defFolder + id.ToString());
        }
        public void UploadCarPhoto(int carId, string path, string fileName)
        {
            string ftpFolder = @"\cars\" + carId.ToString() + @"\" + fileName;
            ftpClient.upload(ftpFolder, path);
        }

        public string GetBufferCopyPath(string fullName, string fileName)
        {
                try
                {
                File.Copy(Path.Combine(fullName.Replace((@"\" + fileName), ""), fileName), Path.Combine(defDir+photoBufferDir, fileName));
            }
                catch (IOException copyError)
                {
                    System.Windows.MessageBox.Show(copyError.Message);
                }

            string str = defDir + photoBufferDir + @"\" + fileName;
            return str;
        }

        public void LoadCarPhotosInBuffer(int id)
        {
            try
            {
                CreateCarFolder(id);
                string[] fullFileNames = ftpClient.getFileNamesFromDirectory("/cars/" + id.ToString()); 
                for (int i = 0; i < fullFileNames.Count(); i++)
                {
                    ftpClient.downloadFiles(fullFileNames[i], defDir + photoBufferDir + @"\" + fullFileNames[i].Split('/')[3]); 
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Ошибка загрузки фотографий для авто");
            }
        }
        public string[] GetBufferPhotos()
        {
            return Directory.GetFiles(defDir + photoBufferDir + @"\");
        }

    }
}

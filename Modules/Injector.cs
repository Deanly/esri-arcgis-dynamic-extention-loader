using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace DynamicDllLoader
{


    public class Injector
    {
        public Injector()
        {
        }

        private object m_Type;
        private Type[] types;
        private int item_order;
        private string filepath;

        public int getItemOrder () { return item_order; }

        public void onForAll ()
        {
            string path = @"D:\Works\Projects\SKT_git\mars_addins\TaskProvider\bin\Debug\TaskProvider.dll";
            int item_order = 1;
            string method = "ExternCall";

            byte[] readAllBytes = File.ReadAllBytes(path);

            if (false == File.Exists(path)) {
                System.Diagnostics.Debug.WriteLine("Error:filepath:" + path);
                return;
            }
            Assembly asm = Assembly.Load(readAllBytes);

            Type[] types = asm.GetExportedTypes();

            m_Type = Activator.CreateInstance(types[item_order]);
            System.Diagnostics.Debug.WriteLine("CreateInstance" + m_Type);

            MethodInfo methodInfo = m_Type.GetType().GetMethod(method);

            if (methodInfo == null)
            {
                System.Diagnostics.Debug.WriteLine("Not found method..");
            }
            else
            {

                methodInfo.Invoke(m_Type, new object[] { } );
            }
        }

        public int Load(string path)
        {
            // @"D:\Works\Projects\SKT_git\mars_addins\TaskProvider\bin\Debug\TaskProvider.dll"
            filepath = path;

            if ( false == File.Exists(filepath) )
            {
                return -1;
            }


            System.Diagnostics.Debug.WriteLine("LoadFrom: " + filepath);
            byte[] readAllBytes = File.ReadAllBytes(filepath);
            Assembly asm = Assembly.Load(readAllBytes);

            if (asm == null)
            {
                return -2;
            }

            types = asm.GetExportedTypes();

            return 0;
        }

        public List<string> GetTypeList()
        {
            List<string> list = new List<string>();

            for(int i = 0; i < types.Length; i++)
            {
                list.Add(types[i].ToString());
            }

            return list;
        }

        public void SelectClass(int n)
        {
            item_order = n;
        }

        public int Excute(string method, params object[] obj)
        {
            System.Diagnostics.Debug.WriteLine("Excute");

            Load(filepath);

            CreateInstance();

            //CallMethod_Del();

            return CallMethod(method, obj);
        }

        private void CreateInstance()
        {
            m_Type = Activator.CreateInstance(types[item_order]);
            System.Diagnostics.Debug.WriteLine("CreateInstance" + m_Type);
        }


        private int CallMethod(string method, params object[] obj)
        {
            if (obj == null) obj = new object[] { };

            MethodInfo methodInfo = m_Type.GetType().GetMethod(method);
            System.Diagnostics.Debug.WriteLine(methodInfo);
            if( methodInfo == null )
            {
                System.Diagnostics.Debug.WriteLine("Not found method..");
                return -2;
            }

            int cntParams = methodInfo.GetParameters().Length;

            if( cntParams == obj.Length)
            {
                methodInfo.Invoke(m_Type, obj);
                return 1;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Miss count of params");
                return -1;
            }
        }

        public delegate void ExternCall();

        private void CallMethod_Del(string method)
        {

            MethodInfo minfo = m_Type.GetType().GetMethod(method);
            ExternCall externCall = (ExternCall)Delegate.CreateDelegate(typeof(ExternCall), null, minfo);
            externCall();

        }
    }

}

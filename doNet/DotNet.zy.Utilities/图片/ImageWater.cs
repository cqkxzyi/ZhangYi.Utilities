using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;


	public enum MarkType
	{
		Text,Image
	}
	/**//// <summary>
	/// 给图片添加水印得类得描述
	/// </summary>
	public class ImageWater
	{
		//---------------------成员变量---------------------        
		private string _text="";
		private string _imgPath="";
		private int _markX=0;
		private int _markY=0;
		private float _transparency=1;
		private string _fontFamily="宋体";
		private Color _textColor=Color.Black;
		private bool _textbold=false;
		//int[] sizes=new int[]{48,32,16,8,6,4};
		int[] sizes=new int[]{28,27,26,25,24,23,22,21,20,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1};
		private Image _image=null;
		private Image _markedIamge=null;
		private MarkType _markType=MarkType.Text;

		/**//// <summary>
		///　初始化一个只添加文字水印得实例
		/// </summary>
		/// <param name="text">水印文字</param>
		/// <param name="fontFamily">文字字体</param>
		/// <param name="bold">是否粗体</param>
		/// <param name="color">字体颜色</param>
		/// <param name="markX">标记位置横坐标</param>
		/// <param name="markY">标记位置纵坐标</param>
		public ImageWater(string text,string fontFamily,bool bold,Color color,int markX,int markY)
		{    
			this._markType=MarkType.Text;
			this._text=text;
			this._fontFamily=fontFamily;
			this._textbold=bold;
			this._textColor=color;
			this._markX=markX;
			this._markY=MarkY;
			this.Mark();
		}
		/**//// <summary>
		/// 实例化一个只添加图片水印得实例
		/// </summary>
		/// <param name="imagePath">水印图片路径</param>
		/// <param name="tranparence">透明度</param>
		/// <param name="markX">标记位置横坐标</param>
		/// <param name="markY">标记位置纵坐标</param>
        public ImageWater(string imagePath, float tranparence, int markX, int markY)
		{
			this._markType=MarkType.Image;        
			this._imgPath=imagePath;
			this._markX=markX;
			this._markY=MarkY;
			this._transparency=tranparence;
			this.Mark();
		}
		 

		//---------------------公共属性---------------------
		/**//// <summary>
		/// 水印类别
		/// </summary>
		public MarkType WaterMarkType
		{
			get
			{
				return _markType;
			}
			set
			{
				_markType=value;
			}
		}        
		/**//// <summary>
		/// 文字水印得内容
		/// </summary>
		public string Text
		{
			get{return _text;}
			set{_text=value;}
		}
		/**//// <summary>
		/// 水印图片得路径
		/// </summary>
		public string WaterImagePath
		{
			get
			{
				return _imgPath;
			}
			set
			{
				this._imgPath=value;
			}
		}
		/**//// <summary>
		/// 水印图片
		/// </summary>
		public Image WaterImage
		{
			get
			{
				try
				{
					return Image.FromFile(this.WaterImagePath);
				}
				catch
				{
					return null;
				}
			}            
		}
		/**//// <summary>
		/// 添加水印位置得横坐标
		/// </summary>
		public int MarkX
		{
			get{return _markX;}
			set{_markX=value;}
		}
		/**//// <summary>
		/// 添加水印位置得纵坐标
		/// </summary>
		public int MarkY
		{
			get{return _markY;}
			set{_markY=value;}
		}
		/**//// <summary>
		/// 水印得透明度
		/// </summary>
		public float Transparency
		{
			get
			{
				if(_transparency>1.0f)
				{
					_transparency=1.0f;
				}
				return _transparency;}
			set{_transparency=value;}
		}
		/**//// <summary>
		/// 水印文字得颜色
		/// </summary>
		public Color TextColor
		{
			get{return _textColor;}
			set{_textColor=value;}
		}
		/**//// <summary>
		/// 水印文字得字体
		/// </summary>
		public string TextFontFamilyStr
		{
			get{return _fontFamily;}
			set{_fontFamily=value;}
		}
		public FontFamily TextFontFamily
		{
			get{return new FontFamily(this.TextFontFamilyStr);}
		}
		/**//// <summary>
		/// 水印文字是否加粗
		/// </summary>
		public bool Bold
		{
			get{return _textbold;}
			set{_textbold=value;}
		}
		/**//// <summary>
		/// 原图
		/// </summary>
    　　public Image SourceImage
	  {
		  get
		  {
			  return _image;
		  }
		  set
		  {
			  _image=value;
		  }
	  }
		/**//// <summary>
		/// 加过水印之后得图
		/// </summary>
		public Image MarkedImage
		{
			get
			{
				return this._markedIamge;
			}            
		}

		 

		//---------------------方法事件---------------------#region ---------------------方法事件---------------------
		public Image Mark(Image img,MarkType markType,string text,Image waterImg,int markx,int marky,
			bool bold,Color textColor,float transparence,FontFamily fontFamily)
		{
			//首先先判断该图片是否是 gif动画，如果为gif动画不对图片进行改动
        
			foreach(Guid guid in img.FrameDimensionsList)
			{
				FrameDimension dimension= new FrameDimension(guid);
				if(img.GetFrameCount(dimension)>1)
				{
					return img;
				}
			}
			try
			{
				//添加文字水印
				if(markType==MarkType.Text)
				{
					//根据源图片生成新的Bitmap对象作为作图区，为了给gif图片添加水印，才有此周折
					Bitmap newBitmap=new Bitmap(img.Width,img.Height,PixelFormat.Format24bppRgb); 
					//设置新建位图得分辨率
					newBitmap.SetResolution(img.HorizontalResolution,img.VerticalResolution);
					//创建Graphics对象，以对该位图进行操作
					Graphics g = Graphics.FromImage(newBitmap);
					//消除锯齿
					g.SmoothingMode=SmoothingMode.AntiAlias;
					//将原图拷贝到作图区
					g.DrawImage(img,new Rectangle(0,0,img.Width,img.Height),0,0,img.Width,img.Height,GraphicsUnit.Pixel);
					//声明字体对象
					Font cFont = null;
					//用来测试水印文本长度得尺子
					SizeF size=new SizeF();
					//探测出一个适合图片大小得字体大小，以适应水印文字大小得自适应
					for(int i=0;i<28;i++)
					{
						//创建一个字体对象
						cFont= new Font(fontFamily,sizes[i]);
						//是否加粗
						if(!bold)
						{
							cFont= new Font(fontFamily,sizes[i],FontStyle.Regular);
						}
						else
						{
							cFont=new Font(fontFamily,sizes[i],FontStyle.Bold);
						}
						//测量文本大小
						size=g.MeasureString(this.Text,cFont);
						//匹配第一个符合要求得字体大小
						if((ushort)size.Width <(ushort)img.Width*1/2)
						{
							break;
						}
					}
					//创建刷子对象，准备给图片写上文字
					Brush brush= new SolidBrush(textColor);
					//在指定得位置写上文字
					g.DrawString(text,cFont,brush,markx,marky);
					//释放Graphics对象
					g.Dispose();   
					//将生成得图片读入MemoryStream
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					newBitmap.Save(ms,ImageFormat.Jpeg);
					//重新生成Image对象
					img=System.Drawing.Image.FromStream(ms);    
					//返回新的Image对象
					return img;

				}
					//添加图像水印
				else if(markType==MarkType.Image)
				{
					//获得水印图像
					Image markImg = waterImg;
					//创建颜色矩阵
					float[][] ptsArray ={ 
											new float[] {1, 0, 0, 0, 0},
											new float[] {0, 1, 0, 0, 0},
											new float[] {0, 0, 1, 0, 0},
											new float[] {0, 0, 0, transparence, 0}, //注意：此处为0.0f为完全透明，1.0f为完全不透明
											new float[] {0, 0, 0, 0, 1}}; 
					ColorMatrix colorMatrix= new ColorMatrix(ptsArray);
					//新建一个Image属性
					ImageAttributes imageAttributes= new ImageAttributes();
					//将颜色矩阵添加到属性
					imageAttributes.SetColorMatrix(colorMatrix,ColorMatrixFlag.Default,
						ColorAdjustType.Default);
					//生成位图作图区
					Bitmap newBitmap=new Bitmap(img.Width,img.Height,PixelFormat.Format24bppRgb);
					//设置分辨率
					newBitmap.SetResolution(img.HorizontalResolution,img.VerticalResolution);
					//创建Graphics
					Graphics g = Graphics.FromImage(newBitmap);
					//消除锯齿
					g.SmoothingMode=SmoothingMode.AntiAlias;
					//拷贝原图到作图区
					g.DrawImage(img,new Rectangle(0,0,img.Width,img.Height),0,0,img.Width,img.Height,GraphicsUnit.Pixel);
					//如果原图过小
					if(markImg.Width>img.Width||markImg.Height>img.Height)
					{
						System.Drawing.Image.GetThumbnailImageAbort callb=null;
						//对水印图片生成缩略图,缩小到原图得1/3
						System.Drawing.Image new_img=markImg.GetThumbnailImage(img.Width/3,markImg.Height*img.Width/markImg.Width,callb,new System.IntPtr());
						//添加水印
						g.DrawImage(new_img,new Rectangle(markx,marky,new_img.Width,new_img.Height),0,0,new_img.Width,new_img.Height,GraphicsUnit.Pixel,imageAttributes);
						//释放缩略图
						new_img.Dispose();
						//释放Graphics
						g.Dispose();
						//将生成得图片读入MemoryStream
						System.IO.MemoryStream ms = new System.IO.MemoryStream();
						newBitmap.Save(ms,ImageFormat.Jpeg);
　　　　　　　　　　//返回新的Image对象
						img=Image.FromStream(ms);
						return img;
					}
						//原图足够大
					else
					{
						//添加水印
						g.DrawImage(markImg,new Rectangle(markx,marky,markImg.Width,markImg.Height),0,0,markImg.Width,markImg.Height,GraphicsUnit.Pixel,imageAttributes);
						//释放Graphics
						g.Dispose();
						//将生成得图片读入MemoryStream
						System.IO.MemoryStream ms = new System.IO.MemoryStream();
						newBitmap.Save(ms,ImageFormat.Jpeg);
						//返回新的Image对象
						img=Image.FromStream(ms);
						return img;
					}                    
				}
				return img;
			}
			catch
			{
				return img;   
			}            

		}
		/**//// <summary>
		/// 添加水印，此方法适用于gif格式得图片
		/// </summary>
		/// <param name="image">需要添加水印得图片</param>
		/// <returns>添加水印之后得图片</returns>
		public void Mark()
		{
			this._markedIamge=Mark(this.SourceImage,this.WaterMarkType,this._text,this.WaterImage,this._markX,this._markY,this._textbold,this._textColor,this._transparency,this.TextFontFamily);
		}
		       
	}

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
	/// ��ͼƬ���ˮӡ���������
	/// </summary>
	public class ImageWater
	{
		//---------------------��Ա����---------------------        
		private string _text="";
		private string _imgPath="";
		private int _markX=0;
		private int _markY=0;
		private float _transparency=1;
		private string _fontFamily="����";
		private Color _textColor=Color.Black;
		private bool _textbold=false;
		//int[] sizes=new int[]{48,32,16,8,6,4};
		int[] sizes=new int[]{28,27,26,25,24,23,22,21,20,19,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1};
		private Image _image=null;
		private Image _markedIamge=null;
		private MarkType _markType=MarkType.Text;

		/**//// <summary>
		///����ʼ��һ��ֻ�������ˮӡ��ʵ��
		/// </summary>
		/// <param name="text">ˮӡ����</param>
		/// <param name="fontFamily">��������</param>
		/// <param name="bold">�Ƿ����</param>
		/// <param name="color">������ɫ</param>
		/// <param name="markX">���λ�ú�����</param>
		/// <param name="markY">���λ��������</param>
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
		/// ʵ����һ��ֻ���ͼƬˮӡ��ʵ��
		/// </summary>
		/// <param name="imagePath">ˮӡͼƬ·��</param>
		/// <param name="tranparence">͸����</param>
		/// <param name="markX">���λ�ú�����</param>
		/// <param name="markY">���λ��������</param>
        public ImageWater(string imagePath, float tranparence, int markX, int markY)
		{
			this._markType=MarkType.Image;        
			this._imgPath=imagePath;
			this._markX=markX;
			this._markY=MarkY;
			this._transparency=tranparence;
			this.Mark();
		}
		 

		//---------------------��������---------------------
		/**//// <summary>
		/// ˮӡ���
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
		/// ����ˮӡ������
		/// </summary>
		public string Text
		{
			get{return _text;}
			set{_text=value;}
		}
		/**//// <summary>
		/// ˮӡͼƬ��·��
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
		/// ˮӡͼƬ
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
		/// ���ˮӡλ�õú�����
		/// </summary>
		public int MarkX
		{
			get{return _markX;}
			set{_markX=value;}
		}
		/**//// <summary>
		/// ���ˮӡλ�õ�������
		/// </summary>
		public int MarkY
		{
			get{return _markY;}
			set{_markY=value;}
		}
		/**//// <summary>
		/// ˮӡ��͸����
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
		/// ˮӡ���ֵ���ɫ
		/// </summary>
		public Color TextColor
		{
			get{return _textColor;}
			set{_textColor=value;}
		}
		/**//// <summary>
		/// ˮӡ���ֵ�����
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
		/// ˮӡ�����Ƿ�Ӵ�
		/// </summary>
		public bool Bold
		{
			get{return _textbold;}
			set{_textbold=value;}
		}
		/**//// <summary>
		/// ԭͼ
		/// </summary>
    ����public Image SourceImage
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
		/// �ӹ�ˮӡ֮���ͼ
		/// </summary>
		public Image MarkedImage
		{
			get
			{
				return this._markedIamge;
			}            
		}

		 

		//---------------------�����¼�---------------------#region ---------------------�����¼�---------------------
		public Image Mark(Image img,MarkType markType,string text,Image waterImg,int markx,int marky,
			bool bold,Color textColor,float transparence,FontFamily fontFamily)
		{
			//�������жϸ�ͼƬ�Ƿ��� gif���������Ϊgif��������ͼƬ���иĶ�
        
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
				//�������ˮӡ
				if(markType==MarkType.Text)
				{
					//����ԴͼƬ�����µ�Bitmap������Ϊ��ͼ����Ϊ�˸�gifͼƬ���ˮӡ�����д�����
					Bitmap newBitmap=new Bitmap(img.Width,img.Height,PixelFormat.Format24bppRgb); 
					//�����½�λͼ�÷ֱ���
					newBitmap.SetResolution(img.HorizontalResolution,img.VerticalResolution);
					//����Graphics�����ԶԸ�λͼ���в���
					Graphics g = Graphics.FromImage(newBitmap);
					//�������
					g.SmoothingMode=SmoothingMode.AntiAlias;
					//��ԭͼ��������ͼ��
					g.DrawImage(img,new Rectangle(0,0,img.Width,img.Height),0,0,img.Width,img.Height,GraphicsUnit.Pixel);
					//�����������
					Font cFont = null;
					//��������ˮӡ�ı����ȵó���
					SizeF size=new SizeF();
					//̽���һ���ʺ�ͼƬ��С�������С������Ӧˮӡ���ִ�С������Ӧ
					for(int i=0;i<28;i++)
					{
						//����һ���������
						cFont= new Font(fontFamily,sizes[i]);
						//�Ƿ�Ӵ�
						if(!bold)
						{
							cFont= new Font(fontFamily,sizes[i],FontStyle.Regular);
						}
						else
						{
							cFont=new Font(fontFamily,sizes[i],FontStyle.Bold);
						}
						//�����ı���С
						size=g.MeasureString(this.Text,cFont);
						//ƥ���һ������Ҫ��������С
						if((ushort)size.Width <(ushort)img.Width*1/2)
						{
							break;
						}
					}
					//����ˢ�Ӷ���׼����ͼƬд������
					Brush brush= new SolidBrush(textColor);
					//��ָ����λ��д������
					g.DrawString(text,cFont,brush,markx,marky);
					//�ͷ�Graphics����
					g.Dispose();   
					//�����ɵ�ͼƬ����MemoryStream
					System.IO.MemoryStream ms = new System.IO.MemoryStream();
					newBitmap.Save(ms,ImageFormat.Jpeg);
					//��������Image����
					img=System.Drawing.Image.FromStream(ms);    
					//�����µ�Image����
					return img;

				}
					//���ͼ��ˮӡ
				else if(markType==MarkType.Image)
				{
					//���ˮӡͼ��
					Image markImg = waterImg;
					//������ɫ����
					float[][] ptsArray ={ 
											new float[] {1, 0, 0, 0, 0},
											new float[] {0, 1, 0, 0, 0},
											new float[] {0, 0, 1, 0, 0},
											new float[] {0, 0, 0, transparence, 0}, //ע�⣺�˴�Ϊ0.0fΪ��ȫ͸����1.0fΪ��ȫ��͸��
											new float[] {0, 0, 0, 0, 1}}; 
					ColorMatrix colorMatrix= new ColorMatrix(ptsArray);
					//�½�һ��Image����
					ImageAttributes imageAttributes= new ImageAttributes();
					//����ɫ������ӵ�����
					imageAttributes.SetColorMatrix(colorMatrix,ColorMatrixFlag.Default,
						ColorAdjustType.Default);
					//����λͼ��ͼ��
					Bitmap newBitmap=new Bitmap(img.Width,img.Height,PixelFormat.Format24bppRgb);
					//���÷ֱ���
					newBitmap.SetResolution(img.HorizontalResolution,img.VerticalResolution);
					//����Graphics
					Graphics g = Graphics.FromImage(newBitmap);
					//�������
					g.SmoothingMode=SmoothingMode.AntiAlias;
					//����ԭͼ����ͼ��
					g.DrawImage(img,new Rectangle(0,0,img.Width,img.Height),0,0,img.Width,img.Height,GraphicsUnit.Pixel);
					//���ԭͼ��С
					if(markImg.Width>img.Width||markImg.Height>img.Height)
					{
						System.Drawing.Image.GetThumbnailImageAbort callb=null;
						//��ˮӡͼƬ��������ͼ,��С��ԭͼ��1/3
						System.Drawing.Image new_img=markImg.GetThumbnailImage(img.Width/3,markImg.Height*img.Width/markImg.Width,callb,new System.IntPtr());
						//���ˮӡ
						g.DrawImage(new_img,new Rectangle(markx,marky,new_img.Width,new_img.Height),0,0,new_img.Width,new_img.Height,GraphicsUnit.Pixel,imageAttributes);
						//�ͷ�����ͼ
						new_img.Dispose();
						//�ͷ�Graphics
						g.Dispose();
						//�����ɵ�ͼƬ����MemoryStream
						System.IO.MemoryStream ms = new System.IO.MemoryStream();
						newBitmap.Save(ms,ImageFormat.Jpeg);
��������������������//�����µ�Image����
						img=Image.FromStream(ms);
						return img;
					}
						//ԭͼ�㹻��
					else
					{
						//���ˮӡ
						g.DrawImage(markImg,new Rectangle(markx,marky,markImg.Width,markImg.Height),0,0,markImg.Width,markImg.Height,GraphicsUnit.Pixel,imageAttributes);
						//�ͷ�Graphics
						g.Dispose();
						//�����ɵ�ͼƬ����MemoryStream
						System.IO.MemoryStream ms = new System.IO.MemoryStream();
						newBitmap.Save(ms,ImageFormat.Jpeg);
						//�����µ�Image����
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
		/// ���ˮӡ���˷���������gif��ʽ��ͼƬ
		/// </summary>
		/// <param name="image">��Ҫ���ˮӡ��ͼƬ</param>
		/// <returns>���ˮӡ֮���ͼƬ</returns>
		public void Mark()
		{
			this._markedIamge=Mark(this.SourceImage,this.WaterMarkType,this._text,this.WaterImage,this._markX,this._markY,this._textbold,this._textColor,this._transparency,this.TextFontFamily);
		}
		       
	}

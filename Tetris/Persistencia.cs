using System;
using sqoClassLibraryAI0502Biblio;

namespace Tetris
{
	public class Persistencia
	{

		private int nColuna0 = 0;
		[ColunaAttribute("Coluna0", "Coluna0", TIPO_COLUNA.tcInt, -1)]
		public int Coluna0
		{
			get { return nColuna0; }
			set { nColuna0 = value; }
		}

		private int nColuna1 = 0;
		[ColunaAttribute("Coluna1", "Coluna1", TIPO_COLUNA.tcInt, -1)]
		public int Coluna1
		{
			get { return nColuna1; }
			set { nColuna1 = value; }
		}

		private int nColuna2 = 0;
		[ColunaAttribute("Coluna2", "Coluna2", TIPO_COLUNA.tcInt, -1)]
		public int Coluna2
		{
			get { return nColuna2; }
			set { nColuna2 = value; }
		}

		private int nColuna3 = 0;
		[ColunaAttribute("Coluna3", "Coluna3", TIPO_COLUNA.tcInt, -1)]
		public int Coluna3
		{
			get { return nColuna3; }
			set { nColuna3 = value; }
		}

		private int nColuna4 = 0;
		[ColunaAttribute("Coluna4", "Coluna4", TIPO_COLUNA.tcInt, -1)]
		public int Coluna4
		{
			get { return nColuna4; }
			set { nColuna4 = value; }
		}

		private int nColuna5 = 0;
		[ColunaAttribute("Coluna5", "Coluna5", TIPO_COLUNA.tcInt, -1)]
		public int Coluna5
		{
			get { return nColuna5; }
			set { nColuna5 = value; }
		}

		private int nColuna6 = 0;
		[ColunaAttribute("Coluna6", "Coluna6", TIPO_COLUNA.tcInt, -1)]
		public int Coluna6
		{
			get { return nColuna6; }
			set { nColuna6 = value; }
		}

		private int nColuna7 = 0;
		[ColunaAttribute("Coluna7", "Coluna7", TIPO_COLUNA.tcInt, -1)]
		public int Coluna7
		{
			get { return nColuna7; }
			set { nColuna7 = value; }
		}

		private int nColuna8 = 0;
		[ColunaAttribute("Coluna8", "Coluna8", TIPO_COLUNA.tcInt, -1)]
		public int Coluna8
		{
			get { return nColuna8; }
			set { nColuna8 = value; }
		}

		private int nColuna9 = 0;
		[ColunaAttribute("Coluna9", "Coluna9", TIPO_COLUNA.tcInt, -1)]
		public int Coluna9
		{
			get { return nColuna9; }
			set { nColuna9 = value; }
		}

	}
}
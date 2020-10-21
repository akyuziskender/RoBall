using UnityEngine;
using UnityEngine.UI;

namespace Game
{
	public class FlexibleGridLayout : LayoutGroup
	{
		public enum FitType {
			Uniform, Width, Height, FixedRows, FixedColumns
		}
		public int rows, columns;
		public Vector2 cellSize, spacing;
		public FitType fitType;
		public bool fitX, fitY;

		public override void CalculateLayoutInputHorizontal() {
			base.CalculateLayoutInputHorizontal();

			if (fitType == FitType.Uniform || fitType == FitType.Width || fitType == FitType.Height) {
				fitX = true;
				fitY = true;

				float sqrt = Mathf.Sqrt(transform.childCount);
				rows = Mathf.CeilToInt(sqrt);
				columns = Mathf.CeilToInt(sqrt);
			}

			if (fitType == FitType.Width || fitType == FitType.FixedColumns) {
				rows = Mathf.CeilToInt(transform.childCount / (float)columns);
			}
			else if (fitType == FitType.Height || fitType == FitType.FixedRows) {
				columns = Mathf.CeilToInt(transform.childCount / (float)rows);
			}

			float parentWidth = rectTransform.rect.width;
			float parentHeight = rectTransform.rect.height;

			float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
			float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

			cellSize.x = fitX ? cellWidth : cellSize.x;
			cellSize.y = fitY ? cellHeight : cellSize.y;

			int columnCount = 0, rowCount = 0;

			for (int i = 0; i < rectChildren.Count; i++) {
				rowCount = i / columns;
				columnCount = i % columns;

				var item = rectChildren[i];

				var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
				var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

				SetChildAlongAxis(item, 0, xPos, cellSize.x);
				SetChildAlongAxis(item, 1, yPos, cellSize.y);
			}
		}

		public override void CalculateLayoutInputVertical() {

		}

		public override void SetLayoutHorizontal() {

		}

		public override void SetLayoutVertical() {

		}
	}
}
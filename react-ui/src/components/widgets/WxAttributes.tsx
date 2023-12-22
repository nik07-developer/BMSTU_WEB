import { CharacterData } from "../../@types/Model"

function WxAttributes(data: CharacterData, posx: number, posy: number) {
	return (
		<div className="widget" style={{ left: posx, top: posy }}>
			{data.attributes.map((attr) => {
				return (
					<div className="subwidget">
						<div className="subwidget-upper">{attr.name_short}</div>
						<div className="subwidget-lower">{attr.value}</div>
					</div>);
			})}
		</div>
	);
}

export default WxAttributes;

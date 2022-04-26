import { DentalGroup } from "../../enums/dental-group.enum";
import { Jaw } from "../../enums/jaw.enum";
import { Quadrant } from "../../enums/quadrant.enum";

export interface ToothApiModel {
    id: string;
    number: number;
    name: string;
    jaw: Jaw;
    quadrant: Quadrant;
    group: DentalGroup;
}

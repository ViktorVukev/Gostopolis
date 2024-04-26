export interface Room {
  id: number;
  number?: string;
  floor?: number;
  roomType: number;
  name: string;
  pricePerNight: number;
  capacity: number;
  beds: string;
  roomAmenities: string;
  bathroomAmenities: string;
}

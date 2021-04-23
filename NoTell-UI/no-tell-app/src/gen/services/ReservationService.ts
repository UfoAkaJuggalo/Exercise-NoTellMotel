/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddReservationGuestVM } from '../models/AddReservationGuestVM';
import type { AddReservationVM } from '../models/AddReservationVM';
import type { AvailableRoomsForDataRangeVM } from '../models/AvailableRoomsForDataRangeVM';
import type { AvailableRoomsWithBedroomsForDataRangeVM } from '../models/AvailableRoomsWithBedroomsForDataRangeVM';
import type { IsRoomAvaliableVM } from '../models/IsRoomAvaliableVM';
import type { ReservationVM } from '../models/ReservationVM';
import type { RoomVM } from '../models/RoomVM';
import { request as __request } from '../core/request';

export class ReservationService {

    /**
     * @param id 
     * @returns any Success
     * @throws ApiError
     */
    public static async deleteReservationService(
id?: number,
): Promise<any> {
        const result = await __request({
            method: 'DELETE',
            path: `/Reservation/remove`,
            query: {
                'id': id,
            },
        });
        return result.body;
    }

    /**
     * @param id 
     * @returns ReservationVM Success
     * @throws ApiError
     */
    public static async getReservationService(
id: number,
): Promise<ReservationVM> {
        const result = await __request({
            method: 'GET',
            path: `/Reservation/get/${id}`,
            errors: {
                500: `Server Error`,
            },
        });
        return result.body;
    }

    /**
     * @param requestBody 
     * @returns boolean Success
     * @throws ApiError
     */
    public static async getReservationService1(
requestBody?: IsRoomAvaliableVM,
): Promise<boolean> {
        const result = await __request({
            method: 'GET',
            path: `/Reservation/isAvailable`,
            body: requestBody,
        });
        return result.body;
    }

    /**
     * @param requestBody 
     * @returns number Success
     * @throws ApiError
     */
    public static async postReservationService(
requestBody?: AddReservationVM,
): Promise<number> {
        const result = await __request({
            method: 'POST',
            path: `/Reservation/add`,
            body: requestBody,
            errors: {
                500: `Server Error`,
            },
        });
        return result.body;
    }

    /**
     * @param requestBody 
     * @returns number Success
     * @throws ApiError
     */
    public static async postReservationService1(
requestBody?: AddReservationGuestVM,
): Promise<number> {
        const result = await __request({
            method: 'POST',
            path: `/Reservation/addResGuest`,
            body: requestBody,
            errors: {
                500: `Server Error`,
            },
        });
        return result.body;
    }

    /**
     * @returns ReservationVM Success
     * @throws ApiError
     */
    public static async getReservationService2(): Promise<Array<ReservationVM>> {
        const result = await __request({
            method: 'GET',
            path: `/Reservation/List`,
        });
        return result.body;
    }

    /**
     * @param requestBody 
     * @returns RoomVM Success
     * @throws ApiError
     */
    public static async postReservationService2(
requestBody?: AvailableRoomsForDataRangeVM,
): Promise<Array<RoomVM>> {
        const result = await __request({
            method: 'POST',
            path: `/Reservation/availableRooms`,
            body: requestBody,
        });
        return result.body;
    }

    /**
     * @param requestBody 
     * @returns RoomVM Success
     * @throws ApiError
     */
    public static async postReservationService3(
requestBody?: AvailableRoomsWithBedroomsForDataRangeVM,
): Promise<Array<RoomVM>> {
        const result = await __request({
            method: 'POST',
            path: `/Reservation/abailableRoomsBedrooms`,
            body: requestBody,
        });
        return result.body;
    }

}
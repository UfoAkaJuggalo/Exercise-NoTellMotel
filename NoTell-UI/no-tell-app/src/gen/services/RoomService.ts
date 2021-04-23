/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { RoomVM } from '../models/RoomVM';
import { request as __request } from '../core/request';

export class RoomService {

    /**
     * @param id 
     * @returns RoomVM Success
     * @throws ApiError
     */
    public static async getRoomService(
id: number,
): Promise<RoomVM> {
        const result = await __request({
            method: 'GET',
            path: `/Room/get/${id}`,
            errors: {
                500: `Server Error`,
            },
        });
        return result.body;
    }

    /**
     * @param bedroomsNumber 
     * @returns RoomVM Success
     * @throws ApiError
     */
    public static async getRoomService1(
bedroomsNumber: number,
): Promise<Array<RoomVM>> {
        const result = await __request({
            method: 'GET',
            path: `/Room/getByBedrooms/${bedroomsNumber}`,
        });
        return result.body;
    }

}
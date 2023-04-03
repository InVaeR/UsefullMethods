def get_trim_box(image, trim_color):
    width, height = image.size
    pixels = image.load()
    left = -1
    upper = -1
    rigth = 0
    lower = 0
    for y in range(height):
        for x in range(width):
            if pixels[x,y] != trim_color:
                lower = y + 1
                if x > rigth:
                    rigth = x+1
                if upper == -1:
                    upper = y
                if left == -1:
                    left = x
                if left != -1 and left > x:
                    left = x

    crop_box = (left, upper, rigth, lower)
    return crop_box
